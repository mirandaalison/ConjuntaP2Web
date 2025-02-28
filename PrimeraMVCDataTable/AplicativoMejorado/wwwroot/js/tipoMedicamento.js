window.onload = function () {
    listarTipoMedicamento();
};

function filtrarTipoMedicamento() {
    let nombre = get("txtTipoMedicamentos");

    if (nombre == "") {
        listarTipoMedicamento();
    } else {
        objTipoMedicamento.url = "TipoMedicamento/filtrarTipoMedicamento/?nombre=" + nombre;
        pintar(objTipoMedicamento);
    }
}

let objSucursales;

async function listarTipoMedicamento() {
    objTipoMedicamento = {
        url: "TipoMedicamento/listarTipoMedicamento",
        cabeceras: ["id Tipo Medicamento", "Nombre", "Descripcion"],
        propiedades: ["idTipoMedicamento", "nombre", "descripcion"],
        editar: true,
        eliminar: true,
        propiedadId: "idTipoMedicamento"
    }

    pintar(objTipoMedicamento);
}

function BuscarTipoMedicamento() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);

    fetchPost("TipoMedicamento/filtrarTipoMedicamento", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}



function LimpiarTipoMedicamento() {
    LimpiarDatos("frmGuardarTipoMedicamento");
    listarTipoMedicamento();
}

function GuardarTipoMedicamento() {
    let forma = document.getElementById("frmGuardarTipoMedicamento");
    let frm = new FormData(forma);

    fetchPost("TipoMedicamento/GuardarTipoMedicamento", "text", frm, function (res) {
        listarTipoMedicamento();
        LimpiarDatos("frmGuardarTipoMedicamento");
    })
}

function Editar(id) {
    fetchGet("TipoMedicamento/recuperarTipoMedicamento/?idTipoMedicamento=" + id, "json", function (data) {
        
        const modalContent = `
        <form id="frmEditarTipoMedicamentoModal" class="row g-3">
            <input type="hidden" name="idTipoMedicamento" value="${data.idTipoMedicamento}">
            <div class="col-md-12">
                <label for="nombreModal" class="form-label">Nombre</label>
                <input type="text" class="form-control" name="nombre" id="nombreModal" value="${data.nombre}" placeholder="Nombre del Medicamento">
            </div>
            <div class="col-md-12">
                <label for="descripcionModal" class="form-label">Descripcion</label>
                <input type="text" class="form-control" name="descripcion" id="descripcionModal" value="${data.descripcion}" placeholder="Descripción del Medicamento">
            </div>
        </form>`;

        Swal.fire({
            title: 'Editar Tipo de Medicamento',
            html: modalContent,
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: 'Guardar Cambios',
            cancelButtonText: 'Cancelar',
            preConfirm: () => {
                const form = document.getElementById('frmEditarTipoMedicamentoModal');
                const formData = new FormData(form);

                return fetch("TipoMedicamento/GuardarCambiosTipoMedicamento", {
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
                listarTipoMedicamento();

                Swal.fire({
                    title: 'Actualizado',
                    text: 'El tipo de medicamento ha sido modificado con éxito.',
                    icon: 'success'
                });
            }
        });
    });
}

function Eliminar(id) {
    showConfirmationModal({
        title: "¿Está seguro?",
        text: "¿Desea eliminar este tipo de medicamento?",
        icon: "warning",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar",
        onConfirm: function () {
            fetchGet("TipoMedicamento/EliminarTipoMedicamento/?idTipoMedicamento=" + id, "text", function (res) {
                listarTipoMedicamento();

                Swal.fire({
                    title: "Eliminado",
                    text: "El tipo de medicamento ha sido eliminado.",
                    icon: "success"
                });
            });
        },
        onCancel: function () {
            Swal.fire({
                title: "Cancelado",
                text: "El tipo de medicamento no se ha modificado.",
                icon: "info"
            });
        }
    });
}