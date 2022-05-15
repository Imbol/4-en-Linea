using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid2D : MonoBehaviour 
{
	private GameObject[,] grid;               
	private int height = 10;                  //Crear alto
	private int width = 10;                   //Crear ancho
	private Color colorUno = Color.white;     //Asignar color
    private Color colorDos = Color.black;     //Asignar color
    private bool playerUno;                 //crear variable booleana
    private Color colorFondo = Color.cyan;    //Asignación del color de fondo de las esferas
    

   
    void Start()
    {
        
		grid = new GameObject[width, height];     //Ubicar el objeto en la altura y ancho
       
	   //Comenzar un bucle para iniciar a crear los parametros de alto y ancho
	    for (int i = 0; i < width; i++)                                      
        {            
            for (int j = 0; j < height; j++)                                  
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);    //Crear objeto tipo esfera
                go.transform.position= new Vector3(i,j,0);                    //Ubicación en el vector
                grid[i,j]=go;                                                 

                go.GetComponent<Renderer>().material.color = colorFondo;      //Asignarle el color del fondo a las esferas creadas

                grid[i, j] = go;                                              
            }

        }
    }

    void Update()
    {
       //Movimiento y posición del mouse
	    Vector3 mPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);           
        UpdatePickedPiece(mPosition);

        if (Input.GetKey(KeyCode.Mouse0))                                                   
        {

        }
    }


    void UpdatePickedPiece(Vector3 position)
    {
        int i = (int)(position.x + 0.5f);      //Ubicación en la posición x
        int j = (int)(position.y + 0.5f);      //Ubicación en la posición y

        if (Input.GetButtonDown("Fire1"))
        {
            if (i >= 0 && j >= 0 && i < width && j< height)                   //Ubicación de ancho y de alto
            {
                GameObject go=grid[i,j];                                      
                if (go.GetComponent<Renderer>().material.color == colorFondo) //se renderisa el color del fondo
                {
                    //Asignaciones del color según el turno
					Color asignarColor = Color.clear;						  
                    if (playerUno)
                    asignarColor = colorUno;

                    else
                    asignarColor = colorDos;

                    go.GetComponent<Renderer>().material.color = asignarColor;
                    playerUno = !playerUno;
					//
                    positionX(i, j, asignarColor);
                    positionY(i, j, asignarColor);
                    diagonalAscen(i, j, asignarColor);
                    diagonalDescen(i, j, asignarColor);
                  

                }
            }
        }
    }
      //verificar si el color en las posiciones horizontales es el mismo
	  
	  public void positionX(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= width)
                continue;

            GameObject go = grid[i, y];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("Ganador");
                }
            }
            else
                contador = 0;
        }
    }

	//verificar si el color en las posiciones vertical es el mismo
    public void positionY(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= height)
                continue;

            GameObject go = grid[x, j];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("Ganador");
                }
            }
            else
                contador = 0;
        }
    }

	//verificar si el color en las posiciones diagonal ascendente es el mismo 
    public void diagonalAscen(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y - 4;

        for (int i = x - 3; i <= x + 3; i++ )
        {
            j++;
            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

                GameObject go =grid[i, j];
                              
                if (go.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    contador++;
                    

                    if (contador == 4)
                    {
                        Debug.Log("Ganador");                         
                	}
                }
                else
                    contador = 0;

        }
    }

		//verificar si el color en las posiciones diagonal descendente es el mismo
    public void diagonalDescen(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y + 4;

        for (int i = x - 3; i <= x + 3; i++)
        {
            j--;

            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

            GameObject go = grid[i, j];

            if (go.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                contador++;
                j--;

                if (contador == 4)
                {
                    Debug.Log("Ganador");
                }
            }
            else
               contador = 0;

        }
    }
}
