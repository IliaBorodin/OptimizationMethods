using GraphSimplex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeLab
{
    public partial class GraphicalMethod : Form
    {
        private List<List<CompositeNumber>> MainTableFraction = new List<List<CompositeNumber>>();
        private List<int> listOfBasis = new List<int>();
        private List<int> listOfParams = new List<int>();
        //Коэф. f(x)
        List<CompositeNumber> listOfFunc = new List<CompositeNumber>();
        // Кол-во ограничений в задаче
        int countOfRest = 0;
        // Тип задачи максимизация/минимизация
        int taskType;
        // Матрица коэффициентов ограничений
        float[,] A;
        // Вектор коэффициентов свободных членов
        float[] B;
        // Вектор коэффициентов целевой функции
        float[] C;
        // Вектор знаков
        int[] S;

        bool taskSolved;

        // Масштаб графика
        float graphScale = 1;
        // Точки, для вычисления правильного масштаба графика
        float Maxsx, Maxsy;

        // Точка, в которой целевая функция достигает максимального/минимального значения
        float sx;
        float sy;

        // Половина ширины и высота графика
        int halfWidth;
        int halfHeight;

        // Шрифты, кисти для рисования графика
        Font drawFont;
        Font smalFont;
        SolidBrush drawBrush;
        StringFormat drawFormat;

        // Буферы для рисования графики
        private BufferedGraphics buffer;
        private BufferedGraphicsContext bufferContext = null;


        public GraphicalMethod(List<List<CompositeNumber>> list, int typeOfTask,
            List<int> listOfBasis, List<int> listOfParams, List<CompositeNumber> listOfFunc)
        {
            this.MainTableFraction = list;
            this.taskType = typeOfTask;
            this.listOfBasis = listOfBasis;
            this.listOfParams = listOfParams;
            this.listOfFunc = listOfFunc;
            InitializeComponents();
            InitializeComponent();
            drawFont = new Font("Arial", 14);
            smalFont = new Font("Arial", 10);
            drawBrush = new SolidBrush(Color.Black);
            drawFormat = new StringFormat();

            bufferContext = BufferedGraphicsManager.Current;
            buffer = bufferContext.Allocate(GraphPaintBox.CreateGraphics(), GraphPaintBox.DisplayRectangle);
            SolveTask();
        }
      
        private void InitializeComponents()
        {
            countOfRest = MainTableFraction.Count - 1;
            A = new float[countOfRest, 2];
            B = new float[countOfRest];
            C = new float[2];
            S = new int[countOfRest];
            for (int i = 0;i<MainTableFraction.Count - 1; ++i)
            {
                for(int j = 0; j < MainTableFraction[i].Count-1; ++j)
                {
                    A[i, j] = MainTableFraction[i][j].GetFloat();
                }
            }
            for(int i =0;i<MainTableFraction.Count - 1; ++i)
            {
                B[i] = MainTableFraction[i][MainTableFraction[0].Count -1].GetFloat();
            }

            for(int i = 0; i<MainTableFraction[0].Count - 1; ++i)
            {
                C[i] = MainTableFraction[MainTableFraction.Count - 1][i].GetFloat();
            }

            for(int i=0;i<MainTableFraction.Count -1; ++i)
            {
                S[i] = 2;
            }
        }

        private void GraphPaintBox_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph(buffer.Graphics);
            buffer.Render(e.Graphics);
        }

        // Определение координат точки пересечения двух линий. Значение функции равно true,
        // если точка пересечения есть, и false, если прямые параллельны.
        private bool LineToPoint(float a1, float b1, float c1, float a2, float b2, float c2, ref PointF pointf)
        {
            float d;
            float dx;
            float dy;

            d = a1 * b2 - b1 * a2;
            if (d != 0)
            {
                dx = -c1 * b2 + b1 * c2;
                dy = -a1 * c2 + c1 * a2;
                pointf.X = -dx / d;
                pointf.Y = -dy / d;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Функция проверяет принаждлежит ли точка, области определения ограничений
        private bool PointBelongsRestDomain(PointF pointf)
        {
            if ((pointf.X < 0) || (pointf.Y < 0))
            {
                return false;
            }

            bool result = true;
            for (int i = 0; i < countOfRest; i++)
            {
                if (!PointBelongsRest(pointf, i))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        // Функция проверяет принаждлежит ли точка, области определения ограничения
        private bool PointBelongsRest(PointF pointf, int ARestIndex)
        {
            float f1 = (A[ARestIndex, 0] * pointf.X) + (A[ARestIndex, 1] * pointf.Y);
            float f2 = B[ARestIndex];

            switch (S[ARestIndex])
            {
                case 0:
                    if (!(f1 == f2))
                    {
                        return false;
                    }
                    break;
                case 1:
                    if (!(f1 >= f2))
                    {
                        return false;
                    }
                    break;
                case 2:
                    if (!(f1 <= f2))
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        // Проверяем прямую на незамкнутость (бесконечное кол-во решений)
        private bool TestForInfinite(float minMaxValue)
        {
            bool result = false;

            PointF pointf = new PointF();
            List<PointF> points = new List<PointF>();

            // Если точка (sx, sy) лежит на оси координат,
            // то передвигаем эту точку на шаг вперед и назад,
            // и добавляем в список.
            if (sx == 0)
            {
                pointf = new PointF();
                pointf.X = 0;
                pointf.Y = sy + 1;
                points.Add(pointf);
                pointf = new PointF();
                pointf.X = 0;
                pointf.Y = sy - 1;
                points.Add(pointf);
            }

            if (sy == 0)
            {
                pointf = new PointF();
                pointf.X = sx + 1;
                pointf.Y = 0;
                points.Add(pointf);
                pointf = new PointF();
                pointf.X = sx - 1;
                pointf.Y = 0;
                points.Add(pointf);
            }

            for (int i = 0; i < countOfRest; i++)
            {
                // Если точка (sx, sy) принадлежит прямой
                if (A[i, 0] * sx + A[i, 1] * sy == B[i])
                {
                    // то составляем параметрическое уравнение прямой
                    if ((A[i, 0] != 0) || (A[i, 1] != 0))
                    {
                        float x1;
                        float y1;
                        float x2;
                        float y2;
                        float alpha1;

                        if (A[i, 1] != 0)
                        {
                            x1 = -1;
                            y1 = (B[i] - A[i, 0] * x1) / A[i, 1];

                            x2 = 1;
                            y2 = (B[i] - A[i, 0] * x2) / A[i, 1];
                        }
                        else
                        {
                            y1 = -1;
                            x1 = (B[i] - A[i, 1] * y1) / A[i, 0];

                            y2 = 1;
                            x2 = (B[i] - A[i, 1] * y2) / A[i, 0];
                        }

                        // Двигаемся по этим прямым с шагом alpha,
                        // и добавляем найденные точки в список.
                        pointf = new PointF();
                        alpha1 = 10;
                        pointf.X = x1 + (x2 - x1) * alpha1;
                        pointf.Y = y1 + (y2 - y1) * alpha1;
                        points.Add(pointf);

                        pointf = new PointF();
                        alpha1 = -10;
                        pointf.X = x1 + (x2 - x1) * alpha1;
                        pointf.Y = y1 + (y2 - y1) * alpha1;
                        points.Add(pointf);
                    }
                }
            }

            // Сравниваем значение целевой функции в найденых точках, с найденным максимумом.
            // Если найден новый допустимый максимум, то задача не замкнута.
            for (int i = 0; i < points.Count; i++)
            {
                pointf = points[i];
                if (PointBelongsRestDomain(pointf))
                {       //если задача на max
                    if (taskType == 0)
                    {
                        result = (pointf.X * C[0] + pointf.Y * C[1]) > minMaxValue;
                    }
                    else
                    {
                        result = (pointf.X * C[0] + pointf.Y * C[1]) < minMaxValue;
                    }

                    if (result)
                    {
                        return true;
                    }
                }
            }

            return result;
        }

        // Рисования сетки
        private void DrawGrid(Graphics g)
        {
            int gridCount;
            float scaleFactor;
            float gridStep = 0;

            // Вычисляем шаг сетки
            for (int i = 1; i <= 30; i++)
            {
                gridStep = graphScale * i;
                if (((gridStep >= 25) && (gridStep <= 30)) || (gridStep > 30))
                {
                    break;
                }
            }

            scaleFactor = gridStep / graphScale;

            // Вычисляем размер сетки
            if (halfHeight > halfWidth)
            {
                gridCount = (int)(halfHeight / gridStep);
            }
            else
            {
                gridCount = (int)(halfWidth / gridStep);
            }

            for (int i = 1; i <= gridCount; i++)
            {
                Pen bluePen = new Pen(Color.Blue, 1);
                bluePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                // Горизонтальные линии
                g.DrawLine(bluePen, 0, halfHeight - (int)gridStep * i, GraphPaintBox.Width, halfHeight - (int)gridStep * i);
                g.DrawLine(bluePen, 0, halfHeight + (int)gridStep * i, GraphPaintBox.Width, halfHeight + (int)gridStep * i);

                // Вертикальные линии
                g.DrawLine(bluePen, halfWidth - (int)gridStep * i, 0, halfWidth - (int)gridStep * i, GraphPaintBox.Height);
                g.DrawLine(bluePen, halfWidth + (int)gridStep * i, 0, halfWidth + (int)gridStep * i, GraphPaintBox.Height);
            }

            for (int i = 1; i <= gridCount; i++)
            {
                g.DrawString(((int)(-i * scaleFactor)).ToString(), smalFont, drawBrush, halfWidth - (int)gridStep * i, halfHeight, drawFormat);
                g.DrawString(((int)(i * scaleFactor)).ToString(), smalFont, drawBrush, halfWidth + (int)gridStep * i, halfHeight, drawFormat);
                g.DrawString(((int)(-i * scaleFactor)).ToString(), smalFont, drawBrush, halfWidth + 5, halfHeight + (int)gridStep * i, drawFormat);
                g.DrawString(((int)(i * scaleFactor)).ToString(), smalFont, drawBrush, halfWidth + 5, halfHeight - (int)gridStep * i, drawFormat);
            }
        }

        // Процедура рисования области определений ограничений
        private void DrawFunctionDomain(Bitmap b, Graphics g)
        {
            DirectBitmap directBitmap = new DirectBitmap(halfWidth, halfHeight);

            for (int x = 0; x < halfWidth; x++)
            {
                for (int y = 0; y < halfHeight; y++)
                {
                    PointF pointF = new PointF(x / graphScale, (halfHeight - y) / graphScale);

                    if (taskSolved)
                    {
                        if (PointBelongsRestDomain(pointF))
                        {
                            directBitmap.SetPixel(x, y, Color.LightGreen);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < countOfRest; i++)
                        {
                            if (PointBelongsRest(pointF, i))
                            {
                                directBitmap.SetPixel(x, y, Color.LightGreen);
                            }
                        }
                    }
                }
            }
            g.DrawImage(directBitmap.Bitmap, new Point(halfWidth, 0));
        }

        // Процедура рисования направляющих линий ограничений
        private void DrawGuideLines(Bitmap b, Graphics g, float x1, float y1, float x2, float y2, int ARestIndex)
        {
            int lineIndex = 0;
            int direction = 0;
            float[] vector1 = new float[2];
            float[] vector2 = new float[2];

            // Промежуток в пикселях, через который будут строиться направляющие линии
            int lineStep = 10;
            // Находим длину отрезка
            double lineLength = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            // Вычисляем кол-во направляющих линий на отрезке
            int lineCount = (int)lineLength / lineStep;

            // Находим единичный вектор данного отрезка
            double vectorLength = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            vector1[0] = (float)((x2 - x1) / vectorLength);
            vector1[1] = (float)((y2 - y1) / vectorLength);

            for (int i = 0; i <= lineCount; i++)
            {
                // Находим координаты точки на отрезке в заданном промежутке
                float ly2;
                float lx2;
                float ly1 = (vector1[1] * lineIndex) + y1;
                float lx1 = (vector1[0] * lineIndex) + x1;

                // Теперь нужно построить прямую перпендикулярную данной
                // Прямая, проходящая через точку (x0;y0) перпендикулярно вектору с координатами (l,m),
                // задаётся уравнением: l(x-x0)+m(y-y0)=0.
                if (vector1[1] != 0)
                {
                    lx2 = lx1 + 5;
                    ly2 = (-vector1[0] * (lx2 - lx1) / vector1[1]) + ly1;
                }
                else
                {
                    ly2 = ly1 + 5;
                    lx2 = (-vector1[1] * (ly2 - ly1) / vector1[0]) + lx1;
                }

                // Находим единичный вектор, перпендикулярный данному отрезку
                vectorLength = Math.Sqrt(Math.Pow(lx1 - lx2, 2) + Math.Pow(ly1 - ly2, 2));
                vector2[0] = (float)((lx1 - lx2) / vectorLength);
                vector2[1] = (float)((ly1 - ly2) / vectorLength);

                if (i == 0)
                {
                    // Находим координаты второй точки отрезка,
                    // перпендикулярного данному
                    ly2 = (vector2[1] * 15) + ly1;
                    lx2 = (vector2[0] * 15) + lx1;

                    // Узнаем направление направляющих линий
                    PointF pointf2 = new PointF((lx2 - halfWidth) / graphScale, (halfHeight - ly2) / graphScale);
                    if (!PointBelongsRest(pointf2, ARestIndex))
                    {
                        direction = -1;
                    }
                    else
                    {
                        direction = 1;
                    }
                }

                // Находим координаты второй точки отрезка,
                // перпендикулярного данному
                ly2 = direction * (vector2[1] * 15) + ly1;
                lx2 = direction * (vector2[0] * 15) + lx1;

                Pen greenPen = new Pen(Color.Green, 1);
                g.DrawLine(greenPen, lx1, ly1, lx2, ly2);

                if (i > 0)
                {
                    lineIndex = lineIndex + lineStep;
                }
            }
        }

        // Рисование направляющего вектора
        private void DrawDirectVector(Graphics g)
        {
            float x, y;
            float x1 = 0, y1 = 0;
            float x2, y2;
            float x3, y3;
            float xp, yp;
            float alpha;
            float xp1, yp1;

            if ((C[0] == 0) && (C[1] == 0))
            {
                return;
            }

            x = C[0];
            y = C[1];

            float vectorLength = (float)Math.Sqrt(x * x + y * y);
            if (x != 0) { x1 = x / vectorLength; }
            if (y != 0) { y1 = y / vectorLength; }

            x = x1;
            y = y1;

            if (halfWidth < halfHeight)
            {
                xp = halfWidth + (x * halfWidth);
                yp = halfHeight + (-y * halfWidth);
            }
            else
            {
                xp = halfWidth + (x * halfHeight);
                yp = halfHeight + (-y * halfHeight);
            }

            Pen redPen = new Pen(Color.Red, 2);
            g.DrawLine(redPen, halfWidth, halfHeight, xp, yp);

            // Рисуем стрелки
            xp1 = -1 * x * 20;
            yp1 = y * 20;

            alpha = 15 * (float)Math.PI / 180;
            x = xp1 * (float)Math.Cos(alpha) - yp1 * (float)Math.Sin(alpha);
            y = xp1 * (float)Math.Sin(alpha) + yp1 * (float)Math.Cos(alpha);

            x2 = xp + x;
            y2 = yp + y;

            g.DrawLine(redPen, xp, yp, x2, y2);

            x3 = x2;
            y3 = y2;

            alpha = -15 * (float)Math.PI / 180;
            x = xp1 * (float)Math.Cos(alpha) - yp1 * (float)Math.Sin(alpha);
            y = xp1 * (float)Math.Sin(alpha) + yp1 * (float)Math.Cos(alpha);

            x2 = xp + x;
            y2 = yp + y;

            g.DrawLine(redPen, xp, yp, x2, y2);
            g.DrawLine(redPen, x2, y2, x3, y3);

            // Если задача иммет решение, то
            // рисуем прямую перпендикулярную направляющему вектору
            if (taskSolved)
            {
                if (C[1] != 0)
                {
                    x1 = -(halfWidth / graphScale);
                    y1 = (-C[0] * (x1 - sx)) / C[1] + sy;

                    x2 = (halfWidth / graphScale);
                    y2 = (-C[0] * (x2 - sx)) / C[1] + sy;
                }
                else
                {
                    y1 = -(halfHeight / graphScale);
                    if (C[0] != 0) { x1 = (-C[1] * (y1 - sy)) / C[0] + sx; }

                    y2 = (halfHeight / graphScale);
                    if (C[0] != 0) { x2 = (-C[1] * (y2 - sy)) / C[0] + sx; }
                }
            }

            g.DrawLine(redPen, halfWidth + (x1 * graphScale), halfHeight + (-y1 * graphScale),
                                halfWidth + (x2 * graphScale), halfHeight + (-y2 * graphScale));
        }

        // Функция решения задачи
        private void SolveTask()
        {
            // Инициализируем переменные
            Maxsx = 0;
            Maxsy = 0;
            taskSolved = false;

            float minMaxValue = float.MaxValue;
            //если задача на max
            if (taskType == 0)
            {
                minMaxValue = float.MinValue;
            }

            List<PointF> points = new List<PointF>();

            // Добавляем точку (0, 0) для проверки на максимальное/минимальное значение
            PointF pointf = new PointF();
            pointf.X = 0;
            pointf.Y = 0;
            points.Add(pointf);

            for (int i = 0; i < countOfRest; i++)
            {
                // Находим точки пересечения прямых (ограничений) с осью координат x1
                if (A[i, 0] != 0)
                {
                    pointf = new PointF();
                    pointf.X = B[i] / A[i, 0];
                    pointf.Y = 0;

                    points.Add(pointf);
                }

                // Находим точки пересечения прямых (ограничений) с осью координат x2
                if (A[i, 1] != 0)
                {
                    pointf = new PointF();
                    pointf.X = 0;
                    pointf.Y = B[i] / A[i, 1];

                    points.Add(pointf);
                }

                // Находим точки пересечения прямых (ограничений) между собой
                for (int j = 0; j < countOfRest; j++)
                {
                    if (i != j)
                    {
                        if (LineToPoint(A[i, 0], A[i, 1], B[i], A[j, 0], A[j, 1], B[j], ref pointf))
                        {
                            pointf = new PointF();
                            points.Add(pointf);
                        }
                    }
                }
            }

            // Среди найденных точек, находим такую, при которой целевая функция принимает
            // максимальное/минимальное значение
            for (int i = 0; i < points.Count; i++)
            {
                pointf = points[i];
                if (PointBelongsRestDomain(pointf))
                {
                    float func = pointf.X * C[0] + pointf.Y * C[1];
                    if (((minMaxValue < func) && (taskType == 0)) ||
                       ((minMaxValue > func) && (taskType == 1)))
                    {
                        sx = pointf.X;
                        sy = pointf.Y;

                        taskSolved = true;
                        minMaxValue = pointf.X * C[0] + pointf.Y * C[1];

                        if (Math.Abs(pointf.X) > Maxsx)
                        {
                            Maxsx = Math.Abs(pointf.X);
                        }

                        if (Math.Abs(pointf.Y) > Maxsy)
                        {
                            Maxsy = Math.Abs(pointf.Y);
                        }
                    }
                }
            }


            textBox.Clear();
            if ((C[0] == 0) && (C[1] == 0))
            {
                textBox.AppendText("Целевая функция не задана!" + Environment.NewLine);
            }
            else
            {
                if (!taskSolved)
                {
                    textBox.AppendText("Задача не имеет допустимого решения! Т.к. ее ограничения несовместны.");
                }
                else
                {
                    if (TestForInfinite(minMaxValue))
                    {
                        textBox.AppendText("Задача имеет бесконечное множество решений! Т.к. ее ограничения не замкнуты и область их определения уходит в бесконечность.");
                    }
                    else
                    {
                        textBox.AppendText("Задача решена!" + Environment.NewLine);
                        textBox.AppendText(Environment.NewLine);
                        textBox.AppendText("Функция:");
                        textBox.AppendText(getFuncStr());
                        if (taskType == 0)
                        {
                            textBox.AppendText("Целевая функция достигает своего максимального значения в точке:" + Environment.NewLine);
                        }
                        else
                        {
                            textBox.AppendText("Целевая функция достигает своего минимального значения в точке:" + Environment.NewLine);
                        }
                        textBox.AppendText(getAnswer());
                        // textBox.AppendText("x1 = " + sx.ToString() + Environment.NewLine);
                        // textBox.AppendText("x2 = " + sy.ToString() + Environment.NewLine);
                        textBox.AppendText(Environment.NewLine);
                        textBox.AppendText("Значение целевой функции:" + Environment.NewLine);
                        

                        textBox.AppendText("f(x1, x2) = " + minMaxValue.ToString());
                    }
                }
            }
        }

        // Рисование графика
        private void DrawGraph(Graphics g)
        {
            Bitmap bmp = new Bitmap(GraphPaintBox.Width, GraphPaintBox.Height);
            Graphics gBmp = Graphics.FromImage(bmp);

            halfWidth = GraphPaintBox.Width / 2;
            halfHeight = GraphPaintBox.Height / 2;
            gBmp.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, GraphPaintBox.Width, GraphPaintBox.Height));

            float maxValue1;
            float maxValue2;
            if (countOfRest > 0)
            {
                if (Maxsx != 0)
                {
                    maxValue1 = (halfWidth / 2) / Maxsx;
                }
                else
                {
                    maxValue1 = float.MaxValue;
                }

                if (Maxsy != 0)
                {
                    maxValue2 = (halfHeight / 2) / Maxsy;
                }
                else
                {
                    maxValue2 = float.MaxValue;
                }

                if (maxValue1 < maxValue2)
                {
                    graphScale = maxValue1;
                }
                else
                {
                    graphScale = maxValue2;
                }

                if (graphScale >= 1000)
                {
                    graphScale = (halfWidth / 2) / 3;
                }
            }
            else
            {
                graphScale = 50.0f;
            }

            if (countOfRest > 0)
            {
                DrawFunctionDomain(bmp, gBmp);
            }

            DrawCoordinateAxis(gBmp);
            DrawGrid(gBmp);

            int pointIndex;
            float[,] xPoints = new float[countOfRest, 4];
            float[,] yPoints = new float[countOfRest, 4];
            bool[] DrawLines = new bool[countOfRest];

            for (int j = 0; j < countOfRest; j++)
            {
                if ((A[j, 0] == 0) && (A[j, 1] == 0))
                {
                    continue;
                }

                pointIndex = 1;
                xPoints = new float[countOfRest, 4];
                yPoints = new float[countOfRest, 4];

                // Находим точку пересечения прямой
                // с левой границей окна
                if (A[j, 1] != 0)
                {
                    float x = halfWidth / graphScale;
                    float y = (B[j] - (A[j, 0] * x)) / A[j, 1];
                    if (Math.Abs(y) <= (halfHeight / graphScale))
                    {
                        xPoints[j, pointIndex] = halfWidth + (x * graphScale);
                        yPoints[j, pointIndex] = halfHeight + (-y * graphScale);
                        pointIndex += 1;
                    }
                }

                // Находим точку пересечения прямой
                // с правой границей окна
                if (A[j, 1] != 0)
                {
                    float x = -halfWidth / graphScale;
                    float y = (B[j] - (A[j, 0] * x)) / A[j, 1];
                    if (Math.Abs(y) <= (halfHeight / graphScale))
                    {
                        xPoints[j, pointIndex] = halfWidth + (x * graphScale);
                        yPoints[j, pointIndex] = halfHeight + (-y * graphScale);
                        pointIndex += 1;
                    }
                }

                // Находим точку пересечения прямой
                // с верхней границей окна
                if (A[j, 0] != 0)
                {
                    float y = halfHeight / graphScale;
                    float x = (B[j] - (A[j, 1] * y)) / A[j, 0];
                    if (Math.Abs(x) <= (halfWidth / graphScale))
                    {
                        xPoints[j, pointIndex] = halfWidth + (x * graphScale);
                        yPoints[j, pointIndex] = halfHeight + (-y * graphScale);
                        pointIndex += 1;
                    }
                }

                // Находим точку пересечения прямой
                // с нижней границей окна
                if (A[j, 0] != 0)
                {
                    float y = -halfHeight / graphScale;
                    float x = (B[j] - (A[j, 1] * y)) / A[j, 0];
                    if (Math.Abs(x) <= (halfWidth / graphScale))
                    {
                        xPoints[j, pointIndex] = halfWidth + (x * graphScale);
                        yPoints[j, pointIndex] = halfHeight + (-y * graphScale);
                        pointIndex += 1;
                    }
                }

                if (pointIndex > 1)
                {
                    DrawLines[j] = true;
                    Pen blackPen2 = new Pen(Color.Black, 2);
                    gBmp.DrawLine(blackPen2, xPoints[j, 1], yPoints[j, 1], xPoints[j, 2], yPoints[j, 2]);
                    DrawGuideLines(bmp, gBmp, xPoints[j, 1], yPoints[j, 1], xPoints[j, 2], yPoints[j, 2], j);
                }
                else
                {
                    DrawLines[j] = false;
                }
            }

            if ((countOfRest > 0) && (taskSolved))
            {
                DrawDirectVector(gBmp);
            }

            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
        }

        // Рисование оси координат
        private void DrawCoordinateAxis(Graphics g)
        {
            g.DrawLine(System.Drawing.Pens.Black,
                       halfWidth, GraphPaintBox.Height,
                       halfWidth, 0);
            g.DrawLine(System.Drawing.Pens.Black,
                       halfWidth, 0,
                       halfWidth - 5, 10);
            g.DrawLine(System.Drawing.Pens.Black,
                       halfWidth - 5, 10,
                       halfWidth + 5, 10);
            g.DrawLine(System.Drawing.Pens.Black,
                       halfWidth + 5, 10,
                       halfWidth, 0);

            g.DrawLine(System.Drawing.Pens.Black,
                       0, halfHeight,
                       GraphPaintBox.Width, halfHeight);
            g.DrawLine(System.Drawing.Pens.Black,
                       GraphPaintBox.Width, halfHeight,
                       GraphPaintBox.Width - 10, halfHeight - 5);
            g.DrawLine(System.Drawing.Pens.Black,
                       GraphPaintBox.Width - 10, halfHeight - 5,
                       GraphPaintBox.Width - 10, halfHeight + 5);
            g.DrawLine(System.Drawing.Pens.Black,
                       GraphPaintBox.Width - 10, halfHeight + 5,
                       GraphPaintBox.Width, halfHeight);

            g.DrawString("x1", drawFont, drawBrush, GraphPaintBox.Width - 30, halfHeight + 30, drawFormat);
            g.DrawString("x2", drawFont, drawBrush, halfWidth + 30, 2, drawFormat);
        }

        private string getAnswer()
        {
            string answer = "f(";
            int count = 1;
            bool flag = true ;
            int i= 0;
            while (count < listOfFunc.Count + 1)
            {
                if (listOfParams.Contains(count))
                {
                    if (flag)
                    {
                        answer += sx.ToString();
                        flag = false;
                    }
                    else
                    {
                        answer += sy.ToString();
                    }
                }
                else
                {
                    float number = B[i] - sx * A[i,0] - sy * A[i,1];
                    answer += number.ToString();
                        ++ i;
                }
                ++count;
                if(count == listOfFunc.Count + 1)
                {
                    answer += ")";
                }
                else
                {
                    answer += ",";
                }
            }
            return answer;
        }
        private string getFuncStr()
        {
            string answer = "f(x) =";
            int count = 1;
            int i = 0;
            while(count < listOfFunc.Count + 1)
            {
                float coef = listOfFunc[i].GetFloat();
                if(coef > 0)
                {
                    answer += "+" + coef.ToString() + "x" + count.ToString();
                }else if(coef == 0)
                {
                    //nixua
                }
                else
                {
                    answer += coef.ToString() + "x" + count.ToString();
                }
                ++i;
                ++count;
            }
            answer += "->";
            if (taskType == 0)
                answer += "max";
            else
                answer += "min";
            return answer;
        }

        
    }
}

