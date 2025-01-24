var libros = [
    { id: 1, titulo: "La NieblA", autor: "Stephen King", genero: "Horror" },
    { id: 2, titulo: "Mito de Sisifo", autor: "Albert Camus", genero: "Filosofia" },
    { id: 3, titulo: "Yo robot", autor: "Issac Asimov", genero: "Ficcion" }
];

var reservados = [];

function mostrarLibrosDisponibles() {
    var contenido = ""; 
    for (var i = 0; i < libros.length; i++) {
        contenido += `
            <li>
                <span><strong>${libros[i].titulo}</strong> por ${libros[i].autor} (${libros[i].genero})</span>
                <button onclick="reservarLibro(${libros[i].id})">Reservar</button>
            </li>
        `;
    }
    document.getElementById("listaLibrosDisponibles").innerHTML = contenido; 
}

function mostrarLibrosReservados() {
    var contenido = ""; 
    for (var i = 0; i < reservados.length; i++) {
        contenido += `
            <li>
                <span><strong>${reservados[i].titulo}</strong> por ${reservados[i].autor}</span>
                <button onclick="devolverLibro(${reservados[i].id})">Devolver</button>
            </li>
        `;
    }
    document.getElementById("listaLibrosReservados").innerHTML = contenido;
}

function buscarLibro() {
    let busqueda = document.getElementById("campoBusqueda").value.toLowerCase();
    let librosFiltrados = libros.filter(function(libro) {
        return libro.titulo.toLowerCase().includes(busqueda);  
    });
    mostrarLibrosDisponibles(librosFiltrados);
}


function reservarLibro(id) {
    for (var i = 0; i < libros.length; i++) {
        if (libros[i].id === id) {
            reservados.push(libros[i]); 
            libros.splice(i, 1);
            mostrarLibrosDisponibles();
            mostrarLibrosReservados();
            mostrarNotificacion("Has reservado el libro '" + reservados[reservados.length - 1].titulo + "'.");
            break;
        }
    }
}

function devolverLibro(id) {
    for (var i = 0; i < reservados.length; i++) {
        if (reservados[i].id === id) {
            libros.push(reservados[i]);
            reservados.splice(i, 1);
            mostrarLibrosDisponibles();
            mostrarLibrosReservados();
            mostrarNotificacion("Has devuelto el libro '" + libros[libros.length - 1].titulo + "'.");
            break;
        }
    }
}

function mostrarNotificacion(mensaje) {
    var notificacion = document.getElementById("notificaciones");
    notificacion.innerHTML = mensaje;
    notificacion.style.display = "block";

    setTimeout(function () {
        notificacion.style.display = "none";
    }, 5000);
}

mostrarLibrosDisponibles();
mostrarLibrosReservados();
