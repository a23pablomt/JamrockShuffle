# GDD

## PRESENTACIÓN/RESUMEN

Clattamount Shuffle nació a raíz de mi interés personal en los juegos de cartas, en especial los del género "Deckbuilder".

El objetivo era crear un sistema de juego que permitiese la existencia de diferentes cartas con habilidades propias, que compartiesen con las demás a través de un sistema de "Palabras Clave". Además de poder jugar partidas contra la propia máquina, el proyecto original contemplaba la posibilidad de hacer pequeños ajustes a tus cartas cada vez que derrotases a tu oponente, como añadir palabras clave, mejorar estadísticas, añadir cartas o retirarlas del mazo. Esto para jugar inmediatamente otra partida con el mazo modificado, siendo la premisa ganar la mayor cantidad de partidas seguidas posible.

Esta clase de juegos tienen un nicho relativamente grande en PC y móvil entre jóvenes adultos y adultos.

## GAMEPLAY

### Objetivos

El objetivo principal del juego es lograr ganar una partida de cartas antes de que tu oponente reduzca tus puntos de vida a 0, reduciendo los suyos antes de que eso pase.

### Jugabilidad

El jugador se encuentra con un sistema que premia el valorar la selección de cartas adecuada para cada situación, ya que solo puede colocar una carta por turno (menos que las que puede colocar su rival). Permite una variedad de 26 cartas, con 4 posiciones posibles para colocarlas y 7 palabras clave que se encuentran en distintas combinaciones.

### Progresión

El juego solo cuenta con una fase, consistente en una partida de cartas contra la máquina, que juega más cartas que el jugador pero sin seguir ninguna estrategia.

### GUI

Durante la partida el jugador puede ver los siguientes elementos:
- Puntos de vida, tanto propios como los del rival.
- Cartas que puede jugar.
- El campo de batalla.
- Cartas que jugará el rival tan rápido como pueda.

## MECÁNICAS

Las reglas del juego son simples: tanto el jugador como el rival cuentan con 20 puntos de vida y una serie de cartas que podrán colocar en 4 casillas del campo. El jugador debe arrastrar las cartas de su mano a la posición donde quiere colocarlas y pulsar "Espacio" para terminar la ronda cuando lo desee. Una vez terminada su ronda, las cartas del jugador golpearán a las del enemigo, realizando su Daño sobre la Vida de las cartas enemigas, tras esto el rival hará lo mismo. Al inicio de su turno el jugador roba una carta. El jugador debe reducir la vida el enemigo a 0 para ganar, si lo opuesto ocurriese, el jugador perdería.

El juego solo tiene una dificultad, que aumenta según se alarga la partida debido a la escasez de cartas del jugador y la presión del rival.

## ELEMENTOS DEL VIDEOJUEGO

El juego está basado en un juego de cartas homónimo jugado en Clattamount, una ciudad fluvial dentro de un mundo de fantasía llamado Luradion que elaboré con el objetivo de jugar partidas de Dragones y Mazmorras con amigos hace años. El juego ficticio no comparte mecánicas con el proyecto, pero todas las cartas pertenecen a este mundo, ya sean figuras legendarias (Chisai "Dama Sombría", Liral de la Lluvia...), personajes relevantes (Idris "Hijo Bastardo"...) o incluso monstruos (Demonio Arácnido...) entre otras cartas más genéricas. El arte de estas cartas ha sido seleccionado para que guardase correlación con los elementos de este mundo y sus regiones principales.

## ASSETS

- Música
- Efectos de sonido
- Modelos 2D/3D
- ...
