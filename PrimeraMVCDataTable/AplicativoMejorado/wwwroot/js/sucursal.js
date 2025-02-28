window.onload = function () {
    listarSucursales();
};
function filtrarSucursales() {
    let nombre = get("txtSucursales");
    if (nombre == "") {
        listarSucursales();
    } else {
        objSucursales.url = "Sucursal/filtrarSucursales/?nombre=" + nombre;
        pintar(objSucursales);
    }

}

let objSucursales;

async function listarSucursales() {
    objSucursales = {
        url: "Sucursal/listarSucursales",
        cabeceras: ["id Sucursal", "Nombre", "Direccion"],
        propiedades: ["idSucursal", "nombre", "direccion"],
        editar: true,
        eliminar: true,
        propiedadId: "idSucursal"
    }
    pintar(objSucursales);
}


function BuscarSucursal() {
    let forma = document.getElementById("frmBusqueda");

    let frm = new FormData(forma);

    fetchPost("Sucursal/filtrarSucursales", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function LimpiarSucursal() {
    LimpiarDatos("frmGuardarSucursal");
    listarSucursales();
}


function GuardarSucursal() {
    let forma = document.getElementById("frmGuardarSucursal");
    let frm = new FormData(forma);
    fetchPost("Sucursal/GuardarSucursal", "text", frm, function (res) {
        listarSucursales();
        LimpiarDatos("frmGuardarSucursal");
        
    })
}



function Editar(id) {
    fetchGet("Sucursal/recuperarSucursal/?idSucursal=" + id, "json", function (data) {
        const modalContent = `
        <form id="frmEditarSucursalModal" class="row g-3">
            <input type="hidden" name="idSucursal" value="${data.idSucursal}">
            <div class="col-md-12">
                <label for="nombreModal" class="form-label">Nombre</label>
                <input type="text" class="form-control" name="nombre" id="nombreModal" value="${data.nombre}" placeholder="Nombre de la Sucursal">
            </div>
            <div class="col-md-12">
                <label for="direccionModal" class="form-label">Dirección</label>
                <input type="text" class="form-control" name="direccion" id="direccionModal" value="${data.direccion}" placeholder="Dirección de la Sucursal">
            </div>
        </form>`;
        
        Swal.fire({
            title: 'Editar Sucursal',
            html: modalContent,
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: 'Guardar Cambios',
            cancelButtonText: 'Cancelar',
            preConfirm: () => {
                const form = document.getElementById('frmEditarSucursalModal');
                const formData = new FormData(form);
                
                return fetch("Sucursal/GuardarCambiosSucursal", {
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
                
                listarSucursales();
                Swal.fire({
                    title: 'Actualizado',
                    text: 'La sucursal ha sido modificada con éxito.',
                    icon: 'success'
                });
            }
        });
    });
}

function Eliminar(id) {
    showConfirmationModal({
        title: "¿Está seguro?",
        text: "¿Desea eliminar esta sucursal?",
        icon: "warning",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar",
        onConfirm: function () {
            fetchGet("Sucursal/EliminarSucursal/?idSucursal=" + id, "text", function (res) {
                listarSucursales();
                Swal.fire({
                    title: "Eliminado",
                    text: "La sucursal ha sido eliminada.",
                    icon: "success"
                });
            });
        },
        onCancel: function () {
            Swal.fire({
                title: "Cancelado",
                text: "La sucursal está segura.",
                icon: "info"
            });
        }
    });
}
