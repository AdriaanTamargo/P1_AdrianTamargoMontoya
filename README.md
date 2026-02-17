# Práctica 1: Plataformas en Unity

Este README explica cómo he planteado y resuelto los diferentes apartados de la práctica, tanto los obligatorios como los opcionales que he intentado implementar (digo intentado ya que uno no ha podido ser).

## 1. Controlador del Jugador (PlayerController)

Para el script de movimiento del personaje, **he partido de la base desarrollada en clase**, adaptándola y ampliándola para cumplir con los requisitos de la práctica.

### Planteamiento
Ninguno propio como tal en el movimiento basico, ya que esto se hizo en clase.

### Apartados opcionales de la práctica
He añadido varias funcionalidades extra al script base para mejorar la jugabilidad:
* **Rotación Suave:** Añadí una interpolación (`Quaternion.Slerp`) para que el personaje se oriente suavemente hacia la dirección en la que camina, evitando giros bruscos.
* **Sprint:** Detectando la tecla `Left Shift`. Esto alterna la velocidad de movimiento entre "caminar" y "correr".
* **Doble Salto:** Añadi una variable que vea si se puede hacer un doble salto (`canDoubleJump`). Esto permite realizar el doble salto en el aire y solo podrás volver a hacer un salto normal o un doble salto si tocas el suelo (detectado por Tag).
* **Animaciones:** Añadí un `Animator` para transicionar entre los estados de *Idle*, *Walk*, *Run* y *Jump*. Para realizar esto tuve que recurrir a videos de YouTube.

---

## 2. Plataforma Móvil (Tipo A)

En este segundo punto, **seguí directamente el desarrollo realizado paso a paso en clase**.

### Funcionamiento
El planteamiento es mover un objeto físico (en este caso una plataforma) entre una posición inicial y un destino marcado por un objeto auxiliar (un cubo llamado destino). El movimiento se realiza en el `FixedUpdate` utilizando `Vector3.MoveTowards`. Cuando la plataforma llega a un punto, se invierten los papeles, se cambia el destino para que la plataforma no se pare de mover.

### Apartado opcionale de la práctica 
* **Que el personaje se mantenga en la plataforma:** He intentado hacer este apartado usando `OnCollisionStay` para hacer hijo momentaneamente (`transform.parent`) al jugador con la plataforma. Sin embargo, **no he conseguido que funcione correctamente**: a pesar de tener el código implementado como vimos en clase el ultimo día (Para hacerlo por mi cuenta consulte la documentacion de Unity, viendo asi que funciones usar).

---

## 3. Plataforma que Cae (Tipo B)

Al principio, pensé que sería muy parecida a la plataforma móvil de clase pero en vertical. Sin embargo, al intentar hacerlo vi que habia que tener cosas en cuenta, como que detecte cuando la plataforma es pisada, que espere hasta caerse...

### Planteamiento y Resolución
Primero pense que habra que crear unas variables para que compruebe si esta plataforma esta cayendo o esta volviendo a su posicion inicial, y para ello cree (`estaCayendo`, `estaReseteando`):

1.  **Detectar al Jugador y que la plataforma se caiga:** Uso `OnCollisionEnter` para detectar al jugador. Para que no se activase por abajo se me ocurrio comprobarlo por altura, ya que es lo que lo que marca la diferencia (que toque arriba o abajo de la plataforma). Añadí una condición para ver si el jugador está **por encima** de la plataforma para evitar que la plataforma se caiga si la tocas por el lateral o por abajo.
2.  **Control de Tiempo:** Guardo el momento del impacto con `Time.time` (lo consulte en la documentación de Unity).
3.  **Caída y Reinicio:**
    * Uso `Vector3.MoveTowards` para bajar la plataforma a la posición final.
    * Al llegar abajo, utilizo `Invoke` (consultado en la documentación) para que se espere un poco antes de volver a la posicion inicial.
