# Memoria tecnica

Nuestro proyecto se ha desarrollado en Unity.
Unity es un entorno de desarrollo 2D y 3D.

Para integrar toda la aplicacion hemos utilizado
dos bibliotecas. La biblioteca TextToSpeech de
Android y la API de DialogFlow en Unity.

### Text to speech

El acceso a la API de TextToSpeech en Android 
se hace a traves de un plugin para Unity. Estos
plugin se escriben en C#. Esos plugin llaman
a los metodos de Java de Android gracias a 
unas clases propias de Unity 

### DialogFlow

Para utilizar DialogFlow se utiliza la API de
Unity desarrollada por el equipo de DialogFlow.
