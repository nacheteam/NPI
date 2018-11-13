# Memoria Descriptiva

**__Autores__**:

Ignacio Aguilera Martos

Diego Asterio de Zaballa

Manuel Enrique López Roldán

## Museo
El museo que hemos diseñado para trabajar con la aplicación es un museo basado en la realidad aumentada. El edificio que requiere la aplicación sólo necesita de una sala que tenga las imágenes que se usarán como etiquetas para superponer los cuadros reales en la aplicación. La idea es poder reutilizar un espacio muy pequeño para albergar múltiples exposiciones en la misma sala.

En el museo contará con distintos postes con sensores NFC, de forma que el usuario al acercar el móvil con las gafas al sensor haga que cambie la exposición completa que se muestra. Esto hace que en un mismo museo se puedan tener múltiples exposiciones y sea fácilmente actualizable, necesitando sólo los modelos de los cuadros para añadirlos a la realidad aumentada.

Tras esto el usuario se coloca las gafas Google Cardboard y puede empezar a mirar por la sala las diferentes etiquetas donde se verán los cuadros asignados a las mismas. Estos cuadros son para el usuario igual que unos reales, puesto que puede acercarse, verlos desde todos los ángulos incluso apreciando los trazos en 3D ya que el modelo que se carga de los cuadros es tridimensional para apreciar la profundidad de los trazos.

Para que la visita sea una actividad que aporte conocimiento y entretenimiento extra al visitante se incorpora también un asistente de voz con el que se puede interactuar sobre cualquier tema relacionado con los cuadros, autores, museo o incluso sobre él mismo. La interacción con el asistente es fácil ya que se activa mediante un botón presente en las gafas que queda accesible al usuario.

Toda esta experiencia hace que traer cuadros de museos muy lejanos entre países y que requieren un espacio muy grande (algunos de ellos llegan a tener varios metros de ancho y alto) se haga muy sencilla ya que no se requiere de un espacio grande ni de una logística de protección y restauración de los cuadros. Además este proyecto hace que las personas de un país con pocos recursos para visitar pinacotecas de países extranjeros tengan una forma económica y cercana a casa para acceder a la cultura.

## Integración de los sensores con la aplicación
Los sensores que hemos empleado hacen que la experiencia del usuario sea muy natural e intuitiva. Para ello hemos integrado la cámara, el NFC, sensor de toque en la pantalla y el giroscopio.

En primer lugar la integración de la cámara ha sido el elemento principal de la aplicación, puesto que es la interfaz del usuario. Para la implementación de la cámara hemos optado por realidad aumentada en vez de realidad virtual cambiando el entorno de la sala por completo. Esto lo hemos hecho para que el usuario pueda moverse por la habitación con confianza de no tropezar y ver qué ocurre a su alrededor. Apuntando la cámara a las distintas imágenes usadas como etiquetas se podrán visualizar los cuadros.

El sensor NFC lo hemos pensado como un elemento de cambio rápido entre dos exposiciones distintas permitiendo que el usuario pueda cambiar entre las mismas sin necesidad de sacar el teléfono de las Cardboard ya que el NFC no lo requiere. Sólo debe tocar el sensor con las gafas para que la exposición cambie para él.

El sensor de toque en la pantalla lo hemos utilizado mediante el botón que tienen las Google Cardboard de forma que una pulsación de ese botón es reconocida por el dispositivo como un tap en la pantalla. De esta forma el usuario puede continuar la experiencia del museo sin tener que sacar el móvil de las gafas ni hacer un gesto molesto con la cabeza para comenzar la asistencia por voz.

Por último, el giroscopio lo hemos utilizado para el momento de finalizar la exposición. Si una persona al finalizar la visita deja las gafas sobre la mesa la aplicación se cerrará automáticamente dejando el dispositivo del usuario en el mismo estado en el que estaba antes de entrar al museo.

## Integración del asistente de voz con la aplicación
El asistente de voz hace que la experiencia sea más enriquecedora para el usuario. Al no tener un acceso directo a los botones y pantalla de su dispositivo móvil debemos hacer que la información que quiera obtener sea dada sin salir de la aplicación, por tanto no sería útil que el usuario saliera de la aplicación para buscar en Internet los datos que quiera saber. El asistente de voz es capaz de resolver este tipo de peticiones donde se le requiere información del cuadro o del autor de forma que, sin necesidad de decirle el usuario sobre qué cuadro o autor quiere hablar, automáticamente sepa el asistente qué cuadro y autor está viendo.

Esta experiencia es muy natural para el usuario puesto que el contexto de la conversación y el contexto visual mantienen la coherencia.

El visitante, además, puede obtener información sobre el museo o hacerle preguntas al asistente sobre él mismo si quiere desconectar un momento de la exposición.

## Mejoras del museo que se pueden implementar en su aplicación real
Actualmente la aplicación no dispone de modelados 3D de los cuadros, pero esto se podría solucionar obteniéndolos de los originales en sus museos. Esta idea es expansible al hecho de mostrar esculturas u otro tipo de arte en 3D de forma detallada al igual que se ofrece en los cuadros.

Además la aplicación podría tener un módulo de integración Bluetooth si dispusiera el museo de un espacio suficiente como para tener una habitación por exposición. De esta forma cada habitación tendría su propio sensor Bluetooth que al ser reconocido por el dispositivo móvil haría cambiar la exposición, siendo aún más natural que la integración con tecnología NFC.
