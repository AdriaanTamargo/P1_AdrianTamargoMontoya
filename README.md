# Práctica 2: Continuación de Plataformas en Unity

En este documento explico cómo he planteado la práctica y la lógica que he utilizado en el código para que todo funcione de forma correcta y sin errores.

## 1 — Sistema de monedas

* **Paso 1 - Crear la moneda:** He creado una moneda con un Collider con el Trigger activado y el script `Coleccionable.cs`. Para saber cuándo la cojo, uso la función `OnTriggerEnter` comprobando que sea el jugador el que choca (Viendo si tiene el código del PlayerController). Si es así, destruyo la moneda (Para que el jugador no la pueda farmear infinitamente). Suma la moneda, aprovecho la función `OnDestroy()`, de forma que le manda el aviso al GameManagerClase justo en el momento en que la moneda desaparece.
* **Paso 2 - Crear el GameManagerClase:** He creado el script `GameManagerClase.cs`. Como necesitaba que mis puntos no se borraran al cambiar de pantalla, he usado la función `DontDestroyOnLoad` dentro del `Start()`, haciendo que este objeto sea "inmortal". Además, para no estar usando la función `Update()` preguntando todo el rato cuántas monedas tengo, he creado un Evento (`Action`). Básicamente, el GameManagerClase solo manda una "señal" cuando recojo una moneda de verdad.
* **Paso 3 - Interfaz de monedas:** He puesto un Canvas con los textos. El script `UIUpdater.cs` lo único que hace es quedarse "escuchando" a que el GameManagerClase mande esa señal. En el `Start()`, el texto mira cuántas monedas hay para poner el número inicial, y a partir de ahí solo se actualiza cuando el GameManagerClase le avisa.

## 2 — Sistema de vidas

* **Paso 1 - Añadir vidas al GameManagerClase:** He metido la variable de las vidas dentro de mi GameManagerClase y he creado otro evento como el de las monedas (Pero en vez de sumar, resta). Si las vidas llegan a 0, carga la pantalla de Game Over.
* **Paso 2 - Crear objeto dañino:** He creado una trampa de pinchos con el script `DamageZone.cs`. Cuando detecta al jugador, hace dos cosas: le dice al GameManagerClase que reste una vida y llama directamente a la función de teletransportar al jugador al punto de partida.
* **Paso 3 - UI de vidas:** He añadido el texto de los corazones al script de la interfaz para que también cambie solo cuando recibe el aviso de daño del GameManagerClase.
* **Paso 4 - Respawn del jugador:** En mi `PlayerController.cs`, guardo mi posición inicial nada más empezar a jugar. Cuando la trampa me hace daño, la función `Respawn()` cambia mis coordenadas a esa posición guardada. Una cosa importante que he añadido en el código es la línea `rb.linearVelocity = Vector3.zero;` para poner la velocidad a cero, así evito que el personaje reaparezca con la inercia del movimiento realizado antes de hacer respawn.

## 3 — Bucle de juego y escenas

* **Paso 1 - Crear escena de menú principal:** En el menú principal he puesto un botón que utiliza mi script `SceneLoader.cs`. Para viajar entre escenas uso la función `SceneManager.LoadScene()` (Indicando a que escena quiero ir). Para arreglar el problema de que al volver a jugar se me guardaban las monedas de la partida anterior, he programado el botón para que limpie y ponga a cero los contadores del GameManagerClase un antes de cargar el nivel.
* **Paso 2 - Crear la meta del nivel:** Al final del mapa he puesto un objeto invisible con un script `Goal.cs`. Si el jugador entra en su Trigger, limpia también los contadores y carga la pantalla de victoria.
* **Paso 3 - Crear escena de victoria:** He montado la escena de victoria con botones de "Volver a Jugar" y "Menú". Lo bueno es que he podido reutilizar mi script de `SceneLoader.cs` arrastrándolo a estos botones, haciendo exactamente la misma función sin tener que repetir código (Pero debo indicar a que escena quiero viajar).
* **Paso 4 - Build Settings:** Para que todo el código de los `SceneManager` funcionara bien y no me diera errores la consola, me he asegurado de registrar el Menú, el Nivel 1 y la Victoria en la ventana de Build Settings de Unity.

## 4 — Escena Opcional de GameOver.
* Como opcionales añadidos simplemente he implementado una escena de Game Over que aparezca cuando el jugador se quede sin vidas. Usa la funcion LoadScene para viajar a la escena de GameOver. Ademas limpia los contadores de vida y de monedas.
