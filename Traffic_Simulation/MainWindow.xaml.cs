using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Simulateur_0._0._2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();

        static List<Voiture> cars = new List<Voiture>();
        
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = TimeSpan.FromMilliseconds(20);
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            float vitessemax = (float)choix_vitessemax.Value;
            float acceleration = (float)choix_acceleration.Value ;
            
            for (int i = 0; i < cars.Count; i++)
            {
                if (i == 0)
                {
                    Canvas.SetLeft(cars[0], cars[0].Move((float)choix_vitessemax.Value, (float)choix_acceleration.Value));
                    Canvas.SetTop(cars[0], cars[0]._yposition);
                }
                else
                {
                    if ((cars[i]._xposition < cars[i - 1]._xposition - 25))
                    {

                        Canvas.SetLeft(cars[i], cars[i].Move(vitessemax, acceleration));
                        Canvas.SetTop(cars[i], cars[i]._yposition);

                    }
                    else
                    {
                        cars[i]._frein = true;
                        Canvas.SetLeft(cars[i], cars[i].Move(vitessemax, acceleration));
                        Canvas.SetTop(cars[i], cars[i]._yposition);

                        cars[i]._frein = false; 
                    }
                }  
            }

            if (cars[0]._xposition >= colonne1.ActualWidth)
            {
                Voiture temp = cars[0];
                cars.RemoveAt(0);
                temp._xposition = 0;
                Canvas.SetLeft(temp, temp.Move(vitessemax, acceleration));
                Canvas.SetTop(temp, temp._yposition); //A VERIFIER
                cars.Add(temp);
            }
 
        }
        
        public void start(object sender, RoutedEventArgs e)
        { 
            timer1.Start();
        }

        public void Ajout_voiture_Click(object sender, RoutedEventArgs e)
        {
            Voiture voiture = new Voiture();
            cars.Add(voiture);
            affichage.Children.Add(voiture);
            Canvas.SetLeft(voiture, voiture._xposition);
            Canvas.SetTop(voiture, voiture._yposition);

        }


        private void choix_vitesse_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vitessemax_choix_affichage.Content = "Vitesse max : " + Math.Round(choix_vitessemax.Value, 3).ToString();
        }

        private void choix_acceleration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            accelerationmax_choix_affichage.Content = "Accélération max : " + Math.Round(choix_acceleration.Value,3).ToString();
        }

        private void Bouton_frein_Click(object sender, RoutedEventArgs e)
        {
            if (cars[0]._frein == false) {
                cars[0]._frein = true;
            }
            else
            {
                cars[0]._frein = false;
            }
        }

        private void Choix_nombrevoitures_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (cars.Count < Choix_nombrevoitures.Value)
            {
                
                int i = 0;
                while (i < Choix_nombrevoitures.Value) {
                   
                    Voiture voiture = new Voiture();
                    cars.Add(voiture);
                    if (rand.Next(100) > 40)
                    {
                        voiture._lane = 0;
                        voiture._yposition = 0;
                    }
                    else
                    {
                        voiture._lane = 1;
                        voiture._yposition = 100;
                    }
                    affichage.Children.Add(voiture);
                    Canvas.SetLeft(voiture, voiture._xposition);
                    Canvas.SetTop(voiture, voiture._yposition);
                    i++;
                }
            }
            else {
                int i = cars.Count-1;
                while (i> Choix_nombrevoitures.Value)
                {
                    {
                        affichage.Children.Remove(cars[i]);
                        cars.RemoveAt(i);
                        i--;
}
                }
                
            }
            
        }
    }
}
