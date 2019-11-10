﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_lab5
{
    public partial class L_systems : Form
    {
        Grammar grammar = new Grammar();
        Pen pen = new Pen(Color.Black);
        Bitmap bmp;
        Graphics g;
        bool is_parsed = false;
        public L_systems()
        {
            InitializeComponent();
            pictureBox_fractal.Image = new Bitmap(pictureBox_fractal.Width, pictureBox_fractal.Height);
            bmp = (Bitmap)pictureBox_fractal.Image;
            pictureBox_fractal.Image = bmp;
            g = Graphics.FromImage(pictureBox_fractal.Image);
            g.Clear(Color.White);
        }

        private void L_systems_Load(object sender, EventArgs e)
        {

        }

        private void parse(string fileName)
        {
            is_parsed = true;
             string[] lines = File.ReadAllLines(fileName);
             string[] firstLineItems = lines[0].Split(' ');
             Grammar temp_gram = new Grammar();
             temp_gram.start_symbol = firstLineItems[0];
             temp_gram.angle = double.Parse(firstLineItems[1]);

             foreach (var line in lines.Skip(1))
             {
                 string[] lineItems = line.Split(' ');
                 temp_gram.rules.Add(lineItems[0][0], lineItems[2]);
             }

            grammar = temp_gram;
        }

        private void draw()
        {
            int depth =(int)numericUpDown_generation.Value;
            string current_state = grammar.start_symbol;
            if (depth != 0)
            {
                for (int i = 0; i < depth; i++)
                {
                    string temp = current_state;
                    current_state = "";
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (grammar.rules.ContainsKey(temp[j]))
                            current_state += grammar.rules[temp[j]];
                        else
                            current_state += temp[j];
                    }
                }
            }

            
            List<PointF> points = new List<PointF>();
            PointF last_point = new PointF(0, pictureBox_fractal.Height/2);
            points.Add(last_point);
            double current_angle = 0;

            Stack<Tuple<PointF, double>> branching_stack = new Stack<Tuple<PointF, double>>(); 

            for (int i = 0; i < current_state.Length; i++)
            {
                char c = current_state[i];
                switch (c)
                {
                    case '-':
                        current_angle += grammar.angle;
                        break;
                    case '+':
                        current_angle -= grammar.angle;
                        break;
                    case '[':
                        branching_stack.Push(new Tuple<PointF, double>(last_point, current_angle));
                        break;
                    case ']':
                        var settings = branching_stack.Pop();
                        last_point = settings.Item1;
                        points.Add(last_point);
                        current_angle = settings.Item2;
                        break;
                    case 'F':
                        {
                            PointF new_point = get_point(last_point, current_angle);
                            points.Add(new_point);
                            last_point = new_point;                              
                            break;
                        }
                }
            }

            double x_min = points.Select(point => point.X).Min();
            double y_min = points.Select(point => point.Y).Min();
            double x_max = points.Select(point => point.X).Max();
            double y_max = points.Select(point => point.Y).Max();
            
            double zoom = Math.Min((pictureBox_fractal.Width - 15) / (x_max - x_min), (pictureBox_fractal.Height - 15) / (y_max - y_min));

            for (int i = 0; i < points.Count; i++)
                points[i] = new PointF((float)((points[i].X - x_min) * zoom + 5), (float)((points[i].Y - y_min) * zoom + 15));

            g.Clear(Color.White);
            for (int i = 1; i < points.Count; i++)
                g.DrawLine(pen, points[i - 1], points[i]);
            pictureBox_fractal.Refresh();
        }

        private PointF get_point(PointF prev_point, double angle)
        {
            angle *= Math.PI / 180.0;
            float delta_X = (float) Math.Cos(angle);
            float delta_Y = (float) Math.Sin(angle);
            return new PointF(prev_point.X + delta_X, prev_point.Y - delta_Y);
        }

        private void button_choose_template_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory.Replace("bin\\Debug", "") + "Templates";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = dialog.FileName;
                label_filename.Visible = true;
                label_filename.Text = Path.GetFileName(selectedFile);
                numericUpDown_generation.Value = 0;
                parse(selectedFile);
                draw();
            }
        }

        private void numericUpDown_generation_ValueChanged(object sender, EventArgs e)
        {
            if(is_parsed)
                draw();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    class Grammar
    {
        public string start_symbol;
        public double angle;
        public Dictionary<char, string> rules = new Dictionary<char, string>();
    }
}
