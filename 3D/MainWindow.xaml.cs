using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _3D
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        double angle = 0.0;
        ModelVisual3D terrain = new ModelVisual3D();

        public MainWindow()
        {
            InitializeComponent();
            Scene3D();

            this.KeyDown += Window_KeyDown;
        }

        private void Scene3D()
        {
            grd.Background = System.Windows.Media.Brushes.Gray;


            // Создание точечного источника освещения
            PointLight pl = new PointLight();
            // Установка цвета света
            pl.Color = Colors.LightYellow;
            // Установка позиции источника
            pl.Position = new Point3D(0, 5, -5);
            // Создание модели, описывающей источник в сцене
            ModelVisual3D light = new ModelVisual3D();
            light.Content = pl;
            // Добавление источника в сцену
            scene.Children.Add(light);

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            Bitmap hMap;
            hMap = new Bitmap(dlg.FileName);

            // Размер ландшафта (256х256 пикселей, как у карты высот)
            const int N = 256;
            // Модель для отображения ландшафта

            MeshGeometry3D geometry = new MeshGeometry3D();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    // Расстановка точек ландшафта
                    double y = hMap.GetPixel(i, j).R / 10.0;
                    geometry.Positions.Add(new Point3D(i, y, j));
                    // Вычисление текстурных координат для точек ландшафта
                    double tu = i / (double)N;
                    double tv = j / (double)N;
                    System.Windows.Point point = new System.Windows.Point(10, 20);

                    geometry.TextureCoordinates.Add(new System.Windows.Point(tu, tv));
                }
            }


            for (int i = 0; i < N - 1; i++)
                for (int j = 0; j < N - 1; j++)
                {
                    // Вычисление индексов 4х точек, находящихся рядом
                    int ind0 = i + j * N;
                    int ind1 = (i + 1) + j * N;
                    int ind2 = i + (j + 1) * N;
                    int ind3 = (i + 1) + (j + 1) * N;
                    // Описание первого треугольника
                    geometry.TriangleIndices.Add(ind0);
                    geometry.TriangleIndices.Add(ind1);
                    geometry.TriangleIndices.Add(ind3);
                    // Описание второго треугольника
                    geometry.TriangleIndices.Add(ind0);
                    geometry.TriangleIndices.Add(ind3);
                    geometry.TriangleIndices.Add(ind2);
                }

            // Создание 3D сцены
            PerspectiveCamera camera = new PerspectiveCamera();
            // Расположение камеры
            camera.Position = new Point3D(N / 2, N / 2, N * 1.5);
            // Точка, на которую направлена камера (центр ландшафта)
            Vector3D lookAt = new Vector3D(N / 2, 0, N / 2);
            camera.LookDirection = Vector3D.Subtract(lookAt, new Vector3D(N / 2, N / 2, N * 2));
            camera.FarPlaneDistance = 1000;
            camera.NearPlaneDistance = 1;
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 75;
            scene.Camera = camera;

            PointLight pl2 = new PointLight();
            pl2.Color = Colors.LightYellow;
            pl2.Position = new Point3D(N, N, N / 2);
            ModelVisual3D light2 = new ModelVisual3D();
            light2.Content = pl2;
            scene.Children.Add(light2);

            terrain.Content = new GeometryModel3D(geometry, new DiffuseMaterial(System.Windows.Media.Brushes.Green));
            scene.Children.Add(terrain);
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            // Если нажата стрелка влево
            if (e.Key == Key.Left)
            {
                angle--;
            }
            // Если нажата стрелка вправо
            if (e.Key == Key.Right)
            {
                angle++;
            }
            // Создание поворота вокруг оси Y на угол angle
            AxisAngleRotation3D ax3d = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
            RotateTransform3D rt = new RotateTransform3D(ax3d);
            // Создание переносов центра ландшафта в центр системы координат и обратно
            int N = 256;
            TranslateTransform3D tr1 = new TranslateTransform3D(-N / 2, 0, -N / 2);
            TranslateTransform3D tr2 = new TranslateTransform3D(N / 2, 0, N / 2);

            Transform3DGroup tg = new Transform3DGroup();
            // Комбинирование преобразований
            tg.Children.Add(tr1);
            tg.Children.Add(rt);
            tg.Children.Add(tr2);
            terrain.Transform = tg;
        }

       
    }
}
