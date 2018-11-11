# Memoria tecnica

Nuestro proyecto se ha desarrollado en Unity.
Unity es un entorno de desarrollo 2D y 3D.

Para integrar toda la aplicacion hemos utilizado
tres bibliotecas. La biblioteca TextToSpeech de
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
o .aar.

Las clases de Unity que nos interesan heredan de
la clase MonoBehaviour y se pueden asignar con la
interfaz de desarrollo a un objeto de la escena.
Este es el metodo usual de programacion en Unity.

En concreto nuestro plugin llama a una clase
de Java a traves de un metodo Speak escrito en
C#. Este metodo a su vez llama al metodo TTSME de 
la clase de Java. Como resultado el telefono 
reproduce un audio con el contenido de una string.

En principio el asistente podria interactuar en
cualquier idioma. Solo se tiene que inicializar
la clase TTS de manera adecuada.

### DialogFlow

Nuestro bot de DialogFlow esta dividido en contextos.
Los contextos son relativos al cuadro y autor que se
visualiza. No hay conflictos entre respuestas porque 
los contextos son independientes entre si.

A continuacion se enumeran los diferentes contextos:

Para el primer autor Goya hay un contexto que sabe
responder a las preguntas relativas a su vida. 
Paralelamente a este contexto existen otros dos que
contienen informacion relativa a las dos obras que
se exponen "Los fusilamientos del 3 de mayo" y 
"pelea a garrotazos". Analogamente tenemos contextos 
para Velazquez y dos obras suyas "Las meninas" y
"La rendicion de Breda".

Ademas de estos contextos especificos de los autores
nuestro bot tambien sabe contestar a preguntas 
relativas a su identidad para que la interaccion
con el agente sea parecida a la interaccion con un 
ser humano. Este tipo de preguntas son "Como te llamas?"
o "Quien eres?".

Otro aspecto interesante de la implementacion es las 
respuestas relativas a el origen del bot.

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
