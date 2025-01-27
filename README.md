Conjunta Parcial 2
Villacrés, Miranda

* Explicacion HTML

<div id="notificaciones"></div>: Crear apartado para notificaciones con un id específico.

<section> : Crear sección para buscar libros.
  
<h2>BUSCAR LIBRO</h2>: Subtítulo de la sección.

<input type="text" id="campoBusqueda" placeholder="Buscar libro: ">: Campo de texto para ingresar el término de búsqueda.

<button>BUSCAR</button>: Botón para iniciar la búsqueda.

<section> : Crear sección para mostrar libros disponibles.
  
<h2>LIBROS DISPONIBLES</h2>: Subtítulo de la sección.

<ul id="listaLibrosDisponibles" class="lista"></ul>: Lista no ordenada para mostrar los libros disponibles.

<section> : Crear sección para mostrar libros reservados.
  
<h2>LIBROS RESERVADOS</h2>: Subtítulo de la sección.

<ul id="listaLibrosReservados" class="lista"></ul>: Lista no ordenada para mostrar los libros reservados.

<script src="Funcionamiento.js"></script>: Enlazar un archivo JavaScript externo llamado Funcionamiento.js para manejar la lógica interactiva de la página.


* Explicación Script
  
mostrarLibrosDisponibles()
Recorre todos los libros en el arreglo libros y crea una lista con el título, autor y género de cada libro.
Añade un botón para reservar junto a cada libro.
Inserta la lista en el contenedor de libros disponibles.


mostrarLibrosReservados()
Recorre todos los libros en el arreglo reservados y crea una lista con el título y autor de cada libro.
Añade un botón para devolver el libro.
Inserta la lista en el contenedor de libros reservados.


buscarLibro()
Toma el valor ingresado en el campo de búsqueda, convierte el texto a minúsculas, filtra los libros que coincidan con el texto de búsqueda en el título.
Actualiza la lista de libros disponibles con los libros filtrados.
El segundo retorno en el filtro no se ejecuta.


reservarLibro(id)
Recorre todos los libros en el arreglo libros, busca el libro que tenga el mismo id que el proporcionado, si lo encuentra, lo mueve a reservados.
Elimina el libro de libros y actualiza las listas de libros disponibles y reservados.
Muestra una notificación con el título del libro reservado.


devolverLibro(id)
Recorre todos los libros en el arreglo reservados, busca el libro que tenga el mismo id que el proporcionado.
Si lo encuentra, lo mueve a libros, elimina el libro de reservados y actualiza las listas de libros disponibles y reservados.
Muestra una notificación con el título del libro devuelto.


mostrarNotificacion(mensaje)
Muestra el mensaje en el contenedor de notificaciones, hace visible la notificación.
La notificación desaparece automáticamente después de 5 segundos.




