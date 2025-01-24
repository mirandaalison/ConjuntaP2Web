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

function reservarLibro(id) {
}

function devolverLibro(id) {
}

function mostrarNotificacion(mensaje) {
}

mostrarLibrosDisponibles();
mostrarLibrosReservados();
