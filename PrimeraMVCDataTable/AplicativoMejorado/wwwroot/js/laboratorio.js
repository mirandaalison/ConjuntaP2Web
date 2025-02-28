window.onload = function () {
    listarLaboratorios();
};

function filtrarLaboratorios() {
    let nombre = get("txtLaboratorio");
    if (nombre == "") {
        listarLaboratorios();
    } else {
        objLaboratorio.url = "Laboratorio/filtrarLaboratorios/?nombre=" + nombre;
        pintar(objLaboratorio);
    }
}

let objLaboratorio;

async function listarLaboratorios() {
    objLaboratorio = {
        url: "Laboratorio/listarLaboratorios",
        cabeceras: ["id Laboratorio", "Nombre", "Direccion", "Persona Contacto"],
        propiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "idLaboratorio"
    }
    pintar(objLaboratorio);
}

function BuscarLaboratorio() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Laboratorio/filtrarLaboratorios", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function GuardarLaboratorio() {
    let forma = document.getElementById("frmGuardarLaboratorio");
    let frm = new FormData(forma);
    fetchPost("Laboratorio/GuardarLaboratorio", "text", frm, function (res) {
        listarLaboratorios();
        LimpiarDatos("frmGuardarLaboratorio");

    })
}

function LimpiarLaboratorio() {
    LimpiarDatos("frmBusqueda");
    listarLaboratorios();
}

function mostrarModalAgregar() {
    const modalContent = `
    <form id="frmAgregarLaboratorioModal" class="row g-3">
        <div class="col-md-12">
            <label for="nombreModal" class="form-label">Nombre</label>
            <input type="text" class="form-control" name="nombre" id="nombreModal" placeholder="Nombre del Laboratorio" required>
        </div>
        <div class="col-md-12">
            <label for="direccionModal" class="form-label">Dirección</label>
            <input type="text" class="form-control" name="direccion" id="direccionModal" placeholder="Dirección del Laboratorio" required>
        </div>
        <div class="col-md-12">
            <label for="personaContactoModal" class="form-label">Persona de Contacto</label>
            <input type="text" class="form-control" name="personaContacto" id="personaContactoModal" placeholder="Persona de Contacto" required>
        </div>
    </form>`;

    Swal.fire({
        title: 'Agregar Nuevo Laboratorio',
        html: modalContent,
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        preConfirm: () => {
            const form = document.getElementById('frmAgregarLaboratorioModal');
            if (!form.checkValidity()) {
                form.reportValidity();
                return false;
            }

            const formData = new FormData(form);

            return fetch("Laboratorio/GuardarLaboratorio", {
                method: 'POST',
                body: formData
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Error al guardar el nuevo laboratorio');
                    }
                    return response.text();
                })
                .catch(error => {
                    Swal.showValidationMessage(`Error: ${error}`);
                });
        }
    }).then((result) => {
        if (result.isConfirmed) {
            listarLaboratorios();
            Swal.fire({
                title: 'Agregado',
                text: 'El nuevo laboratorio ha sido guardado con éxito.',
                icon: 'success'
            });
        }
    });
}

function Editar(id) {
    fetchGet("Laboratorio/recuperarLaboratorio/?idLaboratorio=" + id, "json", function (data) {
        const modalContent = `
        <form id="frmEditarLaboratorioModal" class="row g-3">
            <input type="hidden" name="idLaboratorio" value="${data.idLaboratorio}">
            <div class="col-md-12">
                <label for="nombreModal" class="form-label">Nombre</label>
                <input type="text" class="form-control" name="nombre" id="nombreModal" value="${data.nombre}" placeholder="Nombre del Laboratorio" required>
            </div>
            <div class="col-md-12">
                <label for="direccionModal" class="form-label">Dirección</label>
                <input type="text" class="form-control" name="direccion" id="direccionModal" value="${data.direccion}" placeholder="Dirección del Laboratorio" required>
            </div>
            <div class="col-md-12">
                <label for="personaContactoModal" class="form-label">Persona de Contacto</label>
                <input type="text" class="form-control" name="personaContacto" id="personaContactoModal" value="${data.personaContacto}" placeholder="Persona de Contacto" required>
            </div>
        </form>`;

        Swal.fire({
            title: 'Editar Laboratorio',
            html: modalContent,
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: 'Guardar Cambios',
            cancelButtonText: 'Cancelar',
            preConfirm: () => {
                const form = document.getElementById('frmEditarLaboratorioModal');
                if (!form.checkValidity()) {
                    form.reportValidity();
                    return false;
                }

                const formData = new FormData(form);

                return fetch("Laboratorio/GuardarCambiosLaboratorio", {
                    method: 'POST',
                    body: formData
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error al guardar los cambios');
                        }
                        return response.text();
                    })
                    .catch(error => {
                        Swal.showValidationMessage(`Error: ${error}`);
                    });
            }
        }).then((result) => {
            if (result.isConfirmed) {
                listarLaboratorios();
                Swal.fire({
                    title: 'Actualizado',
                    text: 'El laboratorio ha sido modificado con éxito.',
                    icon: 'success'
                });
            }
        });
    });
}

function Eliminar(id) {
    showConfirmationModal({
        title: "¿Está seguro?",
        text: "¿Desea eliminar este laboratorio?",
        icon: "warning",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar",
        onConfirm: function () {
            fetchGet("Laboratorio/EliminarLaboratorio/?idLaboratorio=" + id, "text", function (res) {
                listarLaboratorios();
                Swal.fire({
                    title: "Eliminado",
                    text: "El laboratorio ha sido eliminado.",
                    icon: "success"
                });
            });
        },
        onCancel: function () {
            Swal.fire({
                title: "Cancelado",
                text: "El laboratorio está seguro.",
                icon: "info"
            });
        }
    });
}
