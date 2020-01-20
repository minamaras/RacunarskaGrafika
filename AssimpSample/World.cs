// -----------------------------------------------------------------------
// <file>World.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2013.</copyright>
// <author>Srđan Mihić</author>
// <author>Aleksandar Josić</author>
// <summary>Klasa koja enkapsulira OpenGL programski kod.</summary>
// -----------------------------------------------------------------------
using System;
using Assimp;
using System.IO;
using System.Reflection;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Core;
using SharpGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Threading;


namespace AssimpSample
{

    


    /// <summary>
    ///  Klasa enkapsulira OpenGL kod i omogucava njegovo iscrtavanje i azuriranje.
    /// </summary>
    public class World : IDisposable
    {
        #region Atributi

        /// <summary>
        ///	 Ugao rotacije Meseca
        /// </summary>
        private float m_moonRotation = 0.0f;

        /// <summary>
        ///	 Ugao rotacije Zemlje
        /// </summary>
        private float m_earthRotation = 0.0f;

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        private AssimpScene m_scene;

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        private float m_xRotation = 0.0f;

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        private float m_yRotation = 0.0f;

        public enum SkaliranjeLopte
        {
            Small,
            Normal,
            Bigger,

        };

        public enum BrzinaRotacije
        {
            Normal,
            Slow,
            Fast
        };

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        private float m_sceneDistance = 950.0f;

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_width;

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_height;

        private int sirina;

        private int visina;
        private int skok = 3;
        private bool onTackasto;
        private bool onReflektor;

        
        private SkaliranjeLopte skaliranje;
        private BrzinaRotacije rotiranje;

        private float x = 0.0f;
        private float y = 0.0f;
        private float z = 0.0f;


        private float stepenRotacije = 0.2f;
        private Boolean kraj = false;
        private float pomeraj = -130f;
        private int parametar = 0;
        private Boolean sut = false;
        private Boolean isJumping = true;


        private enum TextureObjects { Trava = 0, Plastika, Lopta };
        private readonly int m_textureCount = Enum.GetNames(typeof(TextureObjects)).Length;

        /// <summary>
        ///	 Identifikatori OpenGL tekstura
        /// </summary>
        private uint[] m_textures;

        /// <summary>
        ///	 Putanje do slika koje se koriste za teksture
        /// </summary>
        private string[] m_textureFiles = { "..//..//teksture//travaa.jpg", "..//..//teksture//plastikaa.jpg", "..//..//teksture//loptaa.jpg" };

        public static bool OnAnimation = false;

       
        public double ballSize=2;


        #endregion Atributi

        #region Properties

        public bool OnTackasto { get => onTackasto; set => onTackasto = value; }
        public bool OnReflektor { get => onReflektor; set => onReflektor = value; }

        public float X
        {
            get { return x; }
            set { x= value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Boolean Sut
        {
            get { return sut; }
            set { sut = value; }
        }

        public Boolean IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }


        public int Skok
        {
            get { return skok; }
            set { skok = value; }
        }


        public double Velicina
        {
            get { return ballSize; }
            set { ballSize = value; }
        }

        public float StepenRotacije
        {
            get { return stepenRotacije; }
            set { stepenRotacije = value; }
        }

        public Boolean Kraj
        {
            get { return kraj; }
            set { kraj = value; }
        }

        public float Pomeraj
        {
            get { return pomeraj; }
            set { pomeraj = value; }
        }

        public int Parametar
        {
            get { return parametar; }
            set { parametar = value; }
        }

        public SkaliranjeLopte SelectedSkaliranjeLopte
        {
            get { return skaliranje; }
            set
            {

                skaliranje = value;
                Console.WriteLine(skaliranje);

                

            }

       
    }

        public BrzinaRotacije SelectedBrzinaRotacije
        {
            get { return rotiranje; }
            set
            {
                rotiranje = value;
            }
        }

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        public AssimpScene Scene
        {
            get { return m_scene; }
            set { m_scene = value; }
        }



        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return m_xRotation; }
            set { m_xRotation = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        public float RotationY
        {
            get { return m_yRotation; }
            set { m_yRotation = value; }
        }

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        public float SceneDistance
        {
            get { return m_sceneDistance; }
            set { m_sceneDistance = value; }
        }

       


        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        public int Sirina
        {
            get { return sirina; }
            set { sirina = value; }
        }

        public int Visina
        {
            get { return visina; }
            set { visina = value; }
        }

       


        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///  Konstruktor klase World.
        /// </summary>
        public World(String scenePath, String sceneFileName, int width, int height, OpenGL gl)
        {
            this.m_scene = new AssimpScene(scenePath, sceneFileName, gl);
            this.m_width = width;
            this.m_height = height;
            m_textures = new uint[m_textureCount];
        }

        /// <summary>
        ///  Destruktor klase World.
        /// </summary>
        ~World()
        {
            this.Dispose(false);
        }

        #endregion Konstruktori

        #region Metode


 
        /// <summary>
        ///  Korisnicka inicijalizacija i podesavanje OpenGL parametara.
        /// </summary>
        public void Initialize(OpenGL gl)
        {
            
            
            gl.ClearColor(0.2f, 0.37f, 0.65f,1.0f);
            gl.Color(1f, 0f, 0f);
           
            gl.ShadeModel(OpenGL.GL_FLAT);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_CULL_FACE);

            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.ColorMaterial(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT_AND_DIFFUSE);
            gl.Enable(OpenGL.GL_NORMALIZE);

            //SetupLighting(gl);
            //gl.Viewport(0, 0, m_width, m_height);

            #region Teksture
            m_textures = new uint[3];
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_ADD);

            // Ucitaj slike i kreiraj teksture
            gl.GenTextures(m_textureCount, m_textures);
            for (int i = 0; i < m_textureCount; ++i)
            {
                // Pridruzi teksturu odgovarajucem identifikatoru
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[i]);

                // Ucitaj sliku i podesi parametre teksture
                Bitmap image = new Bitmap(m_textureFiles[i]);
                // rotiramo sliku zbog koordinantog sistema opengl-a
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                // RGBA format (dozvoljena providnost slike tj. alfa kanal)
                BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, (int)OpenGL.GL_RGBA8, image.Width, image.Height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, imageData.Scan0);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);      // Linear Filtering
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);      // Linear Filtering

                image.UnlockBits(imageData);
                image.Dispose();
            }
            #endregion

           

            #region pocetak

            OnTackasto = true;
            OnReflektor = true;
            
            #endregion

           

 

            m_scene.LoadScene();
            m_scene.Initialize();
        }


        /// <summary>
        ///  Iscrtavanje OpenGL kontrole.
        /// </summary>
        public void Draw(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.PushMatrix();
            gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Translate(0.0f, 0.0f, -m_sceneDistance);
            gl.Scale(0.5, 0.5, 0.5);

            gl.LookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);

            gl.Rotate(m_xRotation, 2.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);

            gl.Color(0.3f, 0.1f, 0.99f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);


            gl.PushMatrix();
            #region svetlost
            //gl.Enable(OpenGL.GL_LIGHTING);

            gl.PushMatrix();

            //tackasto
            float[] amb = { 1f, 1f, 1f, 1.0f };
            float[] dif = { 1.0f, 1.0f, 1.0f, 1.0f };

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, amb);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, dif);
            

            float[] s = { 0f, 0f, -1f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, s);

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_CUTOFF, 180.0f);
            
            float[] pos = { 600f, 0.0f, 0.0f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos);


            gl.PopMatrix();

            //reflektor
            gl.PushMatrix();

            float[] diff = { 1f, 0.4f, 0.3f,1f };
            float[] ambb = { 1f, 0.4f, 0.3f,1f };
            
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT,
           ambb);
           gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE,
            diff);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_DIRECTION, ambb);

            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_CUTOFF, 30.0f);

            gl.Rotate(-50, 1, 0, 0);

            
            gl.PopMatrix();

            if (OnTackasto)
                gl.Enable(OpenGL.GL_LIGHT0);
            else
                gl.Disable(OpenGL.GL_LIGHT0);

            if (OnReflektor)
                gl.Enable(OpenGL.GL_LIGHT1);
            else
                gl.Disable(OpenGL.GL_LIGHT1);

            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_NORMALIZE);

            gl.PopMatrix();
            #endregion

            #region Lopta

            gl.PushMatrix();
            gl.Scale(ballSize, ballSize, ballSize);
            Skakanje(gl);
            Sutni(gl);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Lopta]);

            m_scene.Draw();
 
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_DIRECTION, new float[] { 0, -1, 0 });


            float[] poss = { 0, 0, m_sceneDistance, 1.0f };
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, poss);

            gl.PopMatrix();


            #endregion



            #region vrh

            gl.PushMatrix();
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Plastika]);

            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(-300, 400f, -150);
            gl.Rotate(90f, 90f, 0f);


            Cylinder cylinderVrh = new Cylinder();
            cylinderVrh.BaseRadius = 10;
            cylinderVrh.TopRadius = 10;
            cylinderVrh.Height = 610;
            cylinderVrh.CreateInContext(gl);

            cylinderVrh.TextureCoords = true;
            cylinderVrh.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();

            #endregion

            #region podloga
            gl.PushMatrix();

            gl.Color(0.39f, 0.99f, 0.37f);

            gl.Translate(0.0f, 90f, -0);
            gl.Rotate(4f, 0f, 0f);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[0]);
            gl.MatrixMode(OpenGL.GL_TEXTURE);

            gl.LoadIdentity();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.Begin(OpenGL.GL_QUADS);

            gl.Normal(0f, 1f, 0f);
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex4f(450f, -100f, 1300, 1);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex4f(450f, -100f, -800, 1);
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex4f(-450f, -100f, -800, 1);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex4f(-450f, -100f, 1300, 1);

            gl.End();

            gl.PopMatrix();

            #endregion

            #region desni stativ

            gl.PushMatrix();
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Plastika]);
            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(300f, 405f, -150f);
            gl.Rotate(90f, 0f, 0f);


            Cylinder cylinderDesniStativ = new Cylinder();
            cylinderDesniStativ.BaseRadius = 10;
            cylinderDesniStativ.TopRadius = 10;
            cylinderDesniStativ.Height = 400;
            cylinderDesniStativ.CreateInContext(gl);

            cylinderDesniStativ.TextureCoords = true;
            cylinderDesniStativ.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();

            #endregion desni stativ

            #region levi stativ

            gl.PushMatrix();

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Plastika]);

            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(-300f, 405f, -150f);
            gl.Rotate(90f, 0f, 0f);

            Cylinder cylinderLeviStativ = new Cylinder();

            cylinderLeviStativ.BaseRadius = 10;
            cylinderLeviStativ.TopRadius = 10;
            cylinderLeviStativ.Height = 400;

            cylinderLeviStativ.CreateInContext(gl);
            cylinderLeviStativ.TextureCoords = true;
            cylinderLeviStativ.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();
            #endregion levi stativ

            #region pozadi stative
            //desna
            gl.PushMatrix();
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, m_textures[(int)TextureObjects.Plastika]);
            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(300f, 390.0f, -152f);
            gl.Rotate(120f, 0.3f, 0.4f);

            Cylinder cylinderDesniPozadi = new Cylinder();

            cylinderDesniPozadi.BaseRadius = 7;
            cylinderDesniPozadi.TopRadius = 7;
            cylinderDesniPozadi.Height = 410;

            cylinderDesniPozadi.CreateInContext(gl);
            cylinderDesniPozadi.TextureCoords = true;
            cylinderDesniPozadi.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();

            //leva
            gl.PushMatrix();
            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(-300f, 390.0f, -152f);
            gl.Rotate(120f, 0.3f, 0.4f);

            Cylinder cylinderLeviPozadi = new Cylinder();

            cylinderLeviPozadi.BaseRadius = 7;
            cylinderLeviPozadi.TopRadius = 7;
            cylinderLeviPozadi.Height = 410;

            cylinderLeviPozadi.CreateInContext(gl);
            cylinderLeviPozadi.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);


            gl.PopMatrix();

            //densno dno gola
            gl.PushMatrix();

            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(305f, 40f, -355f);
            gl.Rotate(190f, 182f, 11f);

            Cylinder cylinderDesniDole = new Cylinder();

            cylinderDesniDole.BaseRadius = 7;
            cylinderDesniDole.TopRadius = 7;
            cylinderDesniDole.Height = 206;

            cylinderDesniDole.CreateInContext(gl);
            cylinderDesniDole.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();


            //levo dno

            gl.PushMatrix();
            gl.Color(0.9f, 0.3f, 0.6f);

            gl.Translate(-298f, 40f, -355f);
            gl.Rotate(190f, 182f, 11f);

            Cylinder cylinderLeviDole = new Cylinder();

            cylinderLeviDole.BaseRadius = 7;
            cylinderLeviDole.TopRadius = 7;
            cylinderLeviDole.Height = 206;

            cylinderLeviDole.CreateInContext(gl);
            cylinderLeviDole.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);

            gl.PopMatrix();

            //sredina
            gl.PushMatrix();

            gl.Color(0.9f, 0.3f, 0.6f);
            gl.Translate(-300, 40f, -355);

            gl.Rotate(90f, 90f, 0f);

            Cylinder cylinderKraj = new Cylinder();

            cylinderKraj.BaseRadius = 7;
            cylinderKraj.TopRadius = 7;
            cylinderKraj.Height = 400;

            cylinderKraj.CreateInContext(gl);
            cylinderVrh.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);


            gl.PopMatrix();

            #endregion pozadi stative

            #region 2d text
            gl.PushMatrix();
            gl.Color(0.0f, 0.0f, 0.0f);
            gl.Viewport(m_width / 2, m_height, m_width, m_height);

            gl.DrawText3D("Tahoma", 10, 0, 0, "");
            gl.DrawText(sirina - 165, visina - 70, 0.0f, 0.0f, 0.0f, "Tahoma", 10, "Predmet: Racunarska grafika");
            gl.DrawText(sirina - 165, visina - 85, 0.0f, 0.0f, 0.0f, "Tahoma", 10, "Sk.god: 2019/20.");
            gl.DrawText(sirina - 165, visina - 100, 0.0f, 0.0f, 0.0f, "Tahoma", 10, "Ime: Mina");
            gl.DrawText(sirina - 165, visina - 115, 0.0f, 0.0f, 0.0f, "Tahoma", 10, "Prezime: Maras");
            gl.DrawText(sirina - 165, visina - 130, 0.0f, 0.0f, 0.0f, "Tahoma", 10, "Sifra zad: 9.1");
            gl.PopMatrix();
            #endregion


            gl.PopMatrix();
            

            gl.Flush();
        }


        private void SetupLighting(OpenGL gl)
        {


            float[] pos = new float[] { 1400.0f, 0f, 0f, 1.0f };
            float[] amb = new float[] { 0.9f, 0.9f, 0.9f, 1.0f };
            float[] dif = new float[] { 0.9f, 0.3f, 0.4f, 1.0f };
            float[] s= { 0f, 0f, -1f };

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, amb);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, dif);         
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, s);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_CUTOFF, 180.0f);
           gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos);
            
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Enable(OpenGL.GL_NORMALIZE);
           

        }

        private void RotiranjeParametar(OpenGL gl)
        {
            

              
            
        }

        private void Skakanje(OpenGL gl)
        {
            if (isJumping)
            {
                

                if (parametar < skok)
                {
                    gl.Translate(0f, pomeraj += 140f, 0.0f);
                    gl.Rotate(stepenRotacije, 0, 0);


                    parametar++;
                }
                else
                {
                    do
                    {

                        gl.Translate(0f, pomeraj -= 140f, 0f);
                        gl.Rotate(stepenRotacije, 0, 0);
                        parametar--;
                    } while (parametar > 0);

                }


            }
        }

        private void Sutni(OpenGL gl)
        {

            if (sut)
            {
                if (isJumping)
                {
                    isJumping = false;
                    gl.Translate(0, 0, 0);
                }
                else
                {
                    if (z > -900f)
                    {
                        x -= 40f;
                        y += 65f;
                        z-= 65f;
                        
                    }
                    else
                    {
                        sut = false;
                        isJumping = true;
                    }

                    gl.Translate(x, y, z);
                    
                }

            }
        }


        /// <summary>
        /// Podesava viewport i projekciju za OpenGL kontrolu.
        /// </summary>
        public void Resize(OpenGL gl, int width, int height)
        {
            m_width = width;
            m_height = height;
            gl.Viewport(0, 0, m_width, m_height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);      // selektuj Projection Matrix
            gl.LoadIdentity();
            gl.Perspective(45f, (double)width / height, 0.5f, 25000f);
            //gl.Perspective(45.0, (double)m_width / (double)m_height, 0.5, 800.0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();                // resetuj ModelView Matrix
        }

        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_scene.Dispose();
            }
        }

        #endregion Metode

        #region IDisposable metode

        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable metode
    }
}
