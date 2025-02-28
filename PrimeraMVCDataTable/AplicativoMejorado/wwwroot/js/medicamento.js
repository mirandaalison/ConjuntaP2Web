window.onload = function () {
    listarMedicamento();
};

function filtrarMedicamentos() {
    let nombre = get("txtMedicamento");
    if (nombre == "") {
        listarMedicamentos();
    } else {
        objMedicamento.url = "Medicamento/filtrarMedicamento/?nombre=" + nombre;
        pintar(objMedicamento);
    }
}

let objMedicamento;

async function listarMedicamento() {
    objMedicamento = {
        url: "Medicamento/listarMedicamentos",
        cabeceras: ["id Medicamento", "Nombre", "Laboratorio", "Tipo Medicamento"],
        propiedades: ["idMedicamento", "nombre", "laboratorio", "tipoMedicamento"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "idMedicamento"
    }
    pintar(objMedicamento);
}

function GuardarMedicamento() {
    let forma = document.getElementById("frmGuardarMedicamento");
    let frm = new FormData(forma);
    fetchPost("Medicamento/GuardarMedicamento", "text", frm, function (res) {
        listarMedicamentos();
        LimpiarDatos("frmGuardarMedicamento");

    })
}

function BuscarMedicamento() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Medicamento/filtrarMedicamentos", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function LimpiarMedicamento() {
    LimpiarDatos("frmBusqueda");
    listarMedicamentos();
}

function mostrarModalAgregar() {
    const modalContent = `
    <form id="frmAgregarMedicamentoModal" class="row g-3">
        <div class="col-md-12">
            <label for="nombreModal" class="form-label">Nombre</label>
            <input type="text" class="form-control" name="nombre" id="nombreModal" placeholder="Nombre del Medicamento" required>
        </div>
        <div class="col-md-12">
            <label for="direccionModal" class="form-label">Laboratorio</label>
            <input type="text" class="form-control" name="laboratorio" id="usoMedicamentoModal" placeholder="Laboratorio" required>
        </div>
        <div class="col-md-12">
            <label for="contenidoModal" class="form-label">Tipo Medicamento</label>
            <input type="text" class="form-control" name="tipoMedicamento" id="contenidoModal" placeholder="Tipo Medicamento" required>
        </div>
    </form>`;

    Swal.fire({
        title: 'Agregar Nuevo Medicamento',
        html: modalContent,
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        preConfirm: () => {
            const form = document.getElementById('frmAgregarMedicamentoModal');

            if (!form.checkValidity()) {
                form.reportValidity();
                return false;
            }

            const formData = new FormData(form);

            return fetch("Medicamento/GuardarMedicamento", {
                method: 'POST',
                body: formData
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Error al guardar el nuevo Medicamento');
                    }
                    return response.text();
                })
                .catch(error => {
                    Swal.showValidationMessage(`Error: ${error}`);
                });
        }
    }).then((result) => {
        if (result.isConfirmed) {
            listarMedicamento();

            Swal.fire({
                title: 'Agregado',
                text: 'El nuevo Medicamento ha sido guardado con éxito.',
                icon: 'success'
            });
        }
    });
}

function Editar(id) {
    fetchGet("Medicamento/recuperarMedicamento/?idMedicamento=" + id, "json", function (data) {
        const modalContent = `
        <form id="frmEditarMedicamentoModal" class="row g-3">
            <input type="hidden" name="idMedicamento" value="${data.idMedicamento}">
            <div class="col-md-12">
                <label for="nombreModal" class="form-label">Nombre</label>
                <input type="text" class="form-control" name="nombre" id="nombreModal" value="${data.nombre}" placeholder="Nombre del Medicamento" required>
            </div>
            <div class="col-md-12">
                <label for="usoMedicamentoModal" class="form-label">Laboratorio</label>
                <input type="text" class="form-control" name="laboratorio" id="usoMedicamentoModal" value="${data.laboratorio}" placeholder="Laboratorio" required>
            </div>
            <div class="col-md-12">
                <label for="contenidoModal" class="form-label">Tipo Medicamento</label>
                <input type="text" class="form-control" name="tipoMedicamento" id="contenidoModal" value="${data.tipoMedicamento}" placeholder="Tipo Medicamento" required>
            </div>
        </form>`;

        Swal.fire({
            title: 'Editar Medicamento',
            html: modalContent,
            icon: 'info',
            showCancelButton: true,
            confirmButtonText: 'Guardar Cambios',
            cancelButtonText: 'Cancelar',
            preConfirm: () => {
                const form = document.getElementById('frmEditarMedicamentoModal');

                if (!form.checkValidity()) {
                    form.reportValidity();
                    return false;
                }

                const formData = new FormData(form);

                return fetch("Medicamento/GuardarCambiosMedicamento", {
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
                listarMedicamentos();

                Swal.fire({
                    title: 'Actualizado',
                    text: 'El Medicamento ha sido modificado con éxito.',
                    icon: 'success'
                });
            }
        });
    });
}