// Funcion para llamar un modal de Bootstrap
function showBSModal(modalId){
    // Crear nuevo componente
    const modal = new bootstrap.Modal(document.getElementById(modalId));

    // Mostrar el modal
    modal.show();
}

// Funcion para cerrar un modal de Bootstrap
function closeBSModal(modalId){
    var modal = new bootstrap.Modal(document.getElementById(modalId));
    modal.hide();
}