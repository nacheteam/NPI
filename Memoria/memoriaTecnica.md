# Memoria tecnica

Nuestro proyecto se ha desarrollado en Unity, un entorno de desarrollo 2D y 3D.

Para integrar toda la aplicacion hemos utilizado
tres bibliotecas: la biblioteca TextToSpeech de
Android, la API de DialogFlow en Unity y Vuforia.

### Text to speech

El acceso a la API de TextToSpeech en Android
se hace a traves de un plugin para Unity. Estos
plugin se escriben en C#. Son plugin que llaman
a los metodos de Java de Android gracias a
unas clases propias de Unity que permiten las
llamadas entre lenguajes. Las funciones de Java
a las que se va a llamar desde C# deben estar
exportadas en una biblioteca de java tipo .jar
o .aar. Esto se hace dentro de android studio
agregando bibliotecas especificas de unity.

Las clases de Unity que nos interesan heredan de
la clase MonoBehaviour y se pueden asignar con la
interfaz de desarrollo a un objeto de la escena.
Este es el metodo usual de programacion en Unity.

Para desarrollar un plugin en Unity se deben tener
dos objetos de c# de tipo "AndroidJavaObject".
Uno contendra el contexto de Android y el otro 
la clase especifica de Android que se debe utilizar,
en nuestro caso la clase con funcionalidad text to 
speech. El metodo que nos permite llamar a una 
funcion de otro lenguaje es CallStatic.

En concreto nuestro plugin llama a una clase
de Java a traves de un metodo Speak escrito en
C#. Este metodo a su vez llama al metodo TTSME de
la clase de Java. Como resultado el telefono
reproduce un audio con el contenido de una string.

En principio el asistente podria interactuar en
cualquier idioma. Solo se tiene que inicializar
la clase TTS de manera adecuada.

### DialogFlow

Nuestro bot de DialogFlow esta dividido en grupos de intents.
Los grupos de intents responden preguntas sobre una temática concreta, ya sea un cuadro, un autor o incluso el propio asisntente.
A continuacion se enumeran los diferentes grupos de intents:

- Autores:
  Los autores que se incluyen en el bot son los mismos que en la exposición: Goya y Velázquez. Las preguntas que responde el bot son las naturales dentro de un museo. Los autores tienen cada uno un grupo de intents distinto para poder distinguir las respuestas que se dan en función del autor. Las preguntas referentes a los autores son:
  - Información referente a los amigos del autor.
  - Información sobre la profundidad de la aportación de dicho autor al panorama de la pintura universal.
  - Intormación referente a las instituciones de estudio de los autores.
  - Información sobre el dinero o la situación económica de los mismos.
  - Información sobre la edad en la que comenzaron a pintar.
  - Información sobe las patologías sufridas por los mismos.
  - Información sobre la esposa o esposo de los autores.
  - Información sobre su familia.
  - Información sobre el género del autor (hombre o mujer).
  - Información sobre la descendencia del autor.
  - Información sobre la madre y el padre del autor.
  - Información sobre el maestro en lo referente a la pintura de dicho autor.
  - Información sobre el movimiento artístico al que pertenecía.
  - Información sobre la fecha y el lugar del fallecimiento del autor.
  - Información sobre la fecha y el lugar de nacimiento del autor.
  - Información sobre su nombre completo.
  - Información sobre el número de obras que publicó.
  - Información sobre las obras consideradas como las más célebres de los autores.
  - Información sobre la ocupación laboral de los mismos.
  - Información sobre el origen de sus padres.
  - Información sobre los países en los que vivió o trabajó el autor.
  - Información sobre la primera obra pintada por el mismo.
  - Información sobre la personalidad (quiénes eran) de los autores.
  - Información sobre la última obra publicada por los autores antes de morir.

  Todas estas preguntas tienen variantes de expresión de forma que casi todas las opciones que el usuario tenga en mente para preguntar algo sean contempladas.
- Cuadros:
  Los cuadros que se incluyen en el bot son los mismos que en la exposición: La rendición de Breda, las Meninas, los Fusilamientos del 3 de Mayo y Duelo a Garrotazos. Cada cuadro tiene su propio grupo de intents de forma que pueda responder distinguiendo las posibles opciones. Las preguntas referentes a los cuadros son:
  - Información sobre quién es el autor del cuadro.
  - Información sobre los colores empleados por el autor en el cuadro.
  - Informacion sobre el contexto histórico de la obra.
  - Información sobre la edad a la que el autor pintó el cuadro.
  - Información sobre el estilo del cuadro.
  - Información sobre la fecha en la que se pintó.
  - Información sobre el mensaje que intentó retratar el autor del cuadro.
  - Información sobre qué cuadro es.
  - Información sobre la ubicación actual del cuadro.
- Musvi:
  El asistente de voz es capaz de responder preguntas sobre sí mismo. Este módulo ha sido creado anticipando que probablemente el usuario no quiera prestar atención durante toda la visita y quiera alguna forma de entretenimiento dentro de la aplicación sin salirse mucho del tema. Para ello se han implementado repuestas al siguiente tipo de preguntas:
  - Información sobre si el bot estará activo durante toda la visita.
  - Cuenta un chiste si se le pide.
  - Cuenta cómo se puede interactuar con él.
  - Responde a la pregunta de quién es su creador o creadores.
  - Responde a la pregunta de si es un hombre o una mujer.
  - Responde sobre la información que es capaz de proporcionar.
  - Si se le insulta el bot da una respuesta.
  - Es capaz de dar información sobre las medidas de seguridad con las que cuenta el edificio del museo.
  - Da información sobre su nombre.
  - Da información sobre el motivo de su creación.
  - Da información sobre su parte preferida del museo.
  - Da información sobre si es una persona.
  - Da información sobre si tiene alguna pregunta prohibida.
  - Responde a la pregunta de quién es.
  - Respode sobre su utilidad
- Museo:
  El asistente de voz responde preguntas relacionadas con el museo como edificio en sí. Las preguntas impementadas en este ámbito son:
  - Qué artistas hay en la exposición.
  - Quién construyó el museo.
  - Qué cuadros hay en el museo.
  - Cuántas exposiciones hay en el mismo.
  - Qué dimensiones tiene el edificio.
  - Cómo está dividido el museo.
  - Qué épocas se recogen en el mismo.
  - Qué estilos pictóricos podemos encontrar en los cuadros.
  - La localización física del museo.
  - La decha de creación.
  - El número de habitaciones del museo y cúales son.
  - Dónde están las habitaciones del museo.
  - El número de cuadros disponibles en el museo.
  - Las obras más importantes del museo.
  - Si el museo es público o privado.
  - El tiempo aproximado de visita.
  - Informa sobre una visita corta al museo en caso de que no tenga tiempo el visitante.

Así mismo cada grupo de intent tiene un contexto asociado, de forma que se puedan hacer preguntas sin especificar a qué van referidas, como por ejemplo hacer preguntas sobre un cuadro o un autor sin que haya una referencia explícita a los mismos.

### Vuforia

Vuforia funciona como una interfaz de realidad aumentada.
Un codigo similar al QR sirve para agregar el cuadro a la
escena. Para interactuar con el cuadro se utilizan botones
con los que se interactua con el cuadro. Se puede hacer
scrolling de la descripcion del cuadro con dos botones.
Estos se activan al mirarlos. Otros botones sirven para
cambiar de cuadro.

### Sensores

Se han usado varios sensores. En primer lugar los sensores
NFC sirven para cambiar el autor de las obras. En nuestro
prototipo solo se usan obras de dos autores pero podria haber
tantos como se quisiese. Otro sensor que se utiliza es la
camara, es primordial para toda la funcionalidad. Nuestra
aplicacion se basa en realidad aumentada, por eso necesitamos
poder tener una camara activa que sirva para observar el mundo.
En ultima instancia se utiliza el giroscopio. Al posar las
Google Cardboard en una superficie la aplicacion se cierra.
Esto constituye una forma natural de acabar el recorrido guiado.

### Referencias
Las librerías empleadas en el proyecto vienen de los siguientes autores y links:

Plugin de TTS: https://github.com/HoseinPorazar/Android-Native-TTS-plugin-for-Unity-3d

Plugin de DialogFlow: https://github.com/dialogflow/dialogflow-unity-client

Librería de relialidad aumentada y virtual:https://www.vuforia.com/
