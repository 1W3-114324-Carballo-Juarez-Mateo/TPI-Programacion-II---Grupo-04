const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});


let TiposDoc = [];
let Sucursales = [];

async function CargarCombos(){
    const cboxTipoDoc = document.getElementById("cboxTipoDoc");
    const cboxSucursal = document.getElementById("cboxSucursal");
    
    const responseDocs = await fetch("https://localhost:7196/api/TipoDoc");
    const dataDocs = await responseDocs.json();
    TiposDoc = dataDocs.entity;

    TiposDoc.forEach(doc => {
        let newOption = document.createElement("option");
        newOption.id = doc.id_tipo_documento;
        newOption.innerText = doc.tipo;
        cboxTipoDoc.appendChild(newOption);
    });
   
    const responseSucursales = await fetch("https://localhost:7196/api/Sucursal");
    const dataSucursales = await responseSucursales.json();
    Sucursales = dataSucursales.entity;

    Sucursales.forEach(sucursal => {
        let newOption = document.createElement("option");
        newOption.id = sucursal.id_sucursal;
        newOption.innerText = sucursal.descripcion;
        cboxSucursal.appendChild(newOption);
    });
}


async function RegistrarEmpleado(){
    if (document.getElementById("textPassword").value !=
    document.getElementById("textPasswordConfirm").value) {
        alert("Las contraseñas no coinciden");
        return;
    }


    let Empleado = {};

    Empleado.id_empleado = 0;
    Empleado.legajo = document.getElementById("textLegajo").value;
    Empleado.nombre = document.getElementById("textNombre").value;
    Empleado.documento = document.getElementById("textDocumento").value;
    const comboSucursales = document.getElementById("cboxSucursal");
    const comboTiposDoc = document.getElementById("cboxTipoDoc");
    Empleado.id_sucursalNavigation = new {
        id_sucursal: comboSucursales.options[comboSucursales.selectedIndex].id,
        descripcion: comboSucursales.options[comboSucursales.selectedIndex].innerText
    }
    Empleado.id_tipo_documentoNavigation = new {
        id_tipo_documento: comboTiposDoc.options[comboTiposDoc.selectedIndex].id,
        tipo: comboTiposDoc.options[comboTiposDoc.selectedIndex].innerText
    }
    Empleado.id_usuarioNavigation = new {
        id_usuario: 0,
        usuario: document.getElementById("textUsuario").value,
        contraseña: document.getElementById("textPassword").value
    }
}