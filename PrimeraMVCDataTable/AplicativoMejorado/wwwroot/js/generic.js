function get(idControl) {
    return document.getElementById(idControl).value;
}

function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    document.getElementsByName(namecontrol)[0].value = valor;
}

function LimpiarDatos(idFormulario) {
    let elementosName = document.querySelectorAll('#' + idFormulario + " [name]");
    let elementoActual;
    let elementoName;
    for (let i = 0; i < elementosName.length; i++) {
        elementoActual = elementosName[i];
        elementoName = elementoActual.name;
        setN(elementoName, "");
    }
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta);
        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        alert("Ocurrió un problema: " + e.message);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        alert("Ocurrio un problema en POST");
    }
}

let objConfiguracionGlobal;
let dataTable;

function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    if (objConfiguracionGlobal.divContenedorTabla == undefined) {
        objConfiguracionGlobal.divContenedorTabla = "divContenedorTabla";
    }
    if (objConfiguracionGlobal.editar == undefined) {
        objConfiguracionGlobal.editar = false;
    }
    if (objConfiguracionGlobal.eliminar == undefined) {
        objConfiguracionGlobal.eliminar = false;
    }
    if (objConfiguracionGlobal.propiedadId == undefined) {
        objConfiguracionGlobal.propiedadId = "";
    }

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "";
        contenido = "<div id='" + objConfiguracionGlobal.divContenedorTabla + "'>";
        contenido += generarTabla(res);
        contenido += "</div>";
        document.getElementById("divTable").innerHTML = contenido;

        setTimeout(() => {
            if (typeof DataTable !== "undefined") {
                if (dataTable) {
                    dataTable.destroy();
                }

                try {
                    dataTable = new DataTable('#tablaData');
                    console.log("DataTables inicializado correctamente");
                } catch (error) {
                    console.error("Error al inicializar DataTables:", error);
                }
            } else {
                console.error("Error: DataTables no está definido. Verifica que los scripts están cargados correctamente.");
            }
        }, 300);
    });
}

function generarTabla(res) {
    let contenido = "";
    let cabeceras = objConfiguracionGlobal.cabeceras;
    let propiedades = objConfiguracionGlobal.propiedades;

    contenido += "<table id='tablaData' class='table table-striped'>";
    contenido += "<thead>";
    contenido += "<tr>";

    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<th>" + cabeceras[i] + "</th>";
    }

    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido += "<th>Operaciones</th>";
    }

    contenido += "</tr>";
    contenido += "</thead>";

    let numRegistros = res.length;
    let obj;
    let propiedadActual;

    contenido += "<tbody>";
    for (let i = 0; i < numRegistros; i++) {
        obj = res[i];
        contenido += "<tr>";

        for (let j = 0; j < propiedades.length; j++) {
            propiedadActual = propiedades[j];
            contenido += "<td>" + obj[propiedadActual] + "</td>";
        }

        if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
            let propiedadID = objConfiguracionGlobal.propiedadId;
            contenido += "<td>";

            if (objConfiguracionGlobal.editar == true) {
                contenido += `<i onclick="Editar(${obj[propiedadID]})" class="btn btn-primary"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
    <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
    </svg> </i>`;
            }

            if (objConfiguracionGlobal.eliminar == true) {
                contenido += `<i onclick="Eliminar(${obj[propiedadID]})" class="btn btn-danger"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
        <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
        </svg> </i>`;
            }

            contenido += "</td>";
        }

        contenido += "</tr>";
    }

    contenido += "</tbody>";
    contenido += "</table>";
    return contenido;
}

function showConfirmationModal(options) {
    const {
        title = "Are you sure?",
        text = "You won't be able to revert this!",
        icon = "warning",
        confirmButtonText = "Yes, proceed!",
        cancelButtonText = "Cancel",
        onConfirm,
        onCancel
    } = options;

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success me-2",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: title,
        text: text,
        icon: icon,
        showCancelButton: true,
        confirmButtonText: confirmButtonText,
        cancelButtonText: cancelButtonText,
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            if (onConfirm) onConfirm();
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            if (onCancel) onCancel();
        }
    });
}