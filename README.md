# Test-Snake
Pequeño juego tipo snake con uso de base de datos SQlite para guardad marcadores del menor a mayor.


# Uso

El proyecto cuenta con un splash de inicio el cual nos lleva a un menú principal donde se encuentra las opciones de:

#START GAME
#SCORE
#OPTIONS
#QUIT GAME

La opción de “START GAME” te lleva al inicio del juego Snake en el cual tiene que pulsar alguna flecha del teclado en cualquier dirección o con el botón del ratón para poder iniciar. Esta pantalla cuenta con un marcador el cual se va incrementando con forme se colisiona con las manzanas y un Hiscore, el cual guarda el marcador reciente en un fichero dentro de unity. 
	Así mismo cuenta con un “menú de pausa” dentro del juego para pode salir o parar el juego en cualquier momento, este se accede con la tecla “ESC” y se puede controlar por medio de pulsaciones por el ratón.
 Por último, tiene un sistema para guardar score dentro de una base de datos SQlite, la cual al colisionar contra algún muro o contra el mismo guarda tu score obtenido y te pide tu nombre para regístralo con este.

La opción “SCORE” muestra los puntajes guardados de mayor a menor, este esta configurado para mostrar los primeros 15 mejores marcadores, esto ocasiona que los marcadores más bajos sean eliminados por los marcadores más altos.
	Cuenta con un par de botones los cuales permiten desplazarse por los registros de marcadores y un botón de back para regresar al menú anterior.

El botón “Opción” esta subdividido en 2 opciones audio y dificultad en los cuales nos permite controlar el volumen de la música y controlar la velocidad de l Snake.
