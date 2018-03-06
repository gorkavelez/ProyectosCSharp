function ConfirmAction(action) {
    return (confirm('¿Confirma que desea ' + action + '?'));
}

function DatosSinRegistrar() {
    var hdcontrol = document.getElementById('ctl00_ContentPlaceHolder1_hddDatosSinRegistrar');
    var datos = hdcontrol.value;
    if (datos == "True") {
        return (confirm('Quedan datos sin registrar, que se perderán. ¿Confirma que desea cerrar el formulario?'));
    }
    else {
        return (true);
    }
}

function DatoInformado(ctlName) {
    var ctl = document.getElementById(ctlName);
    return (ctl.value != "");
}

function DatosObligatorios() {

    if (!DatoInformado('ctl00_ContentPlaceHolder1_hdCodOperario')) {
        alert('No se ha especificado Operario');
        return (false);
    }
    if (!DatoInformado('ctl00_ContentPlaceHolder1_hdCodMaquina')) {
        alert('No se ha especificado Máquina');
        return (false);
    }
    if (!DatoInformado('ctl00_ContentPlaceHolder1_hdCodCliente')) {
        alert('No se ha especificado Cliente');
        return (false);
    }

    return (true);
}

function FocusScript(clientID){
    document.getElementById(clientID).focus();
    document.getElementById(clientID).select();
}

function FocusScriptWithAction(clientID,tipo,titulo) {
    FocusScript(clientID);
    __doPostBack(tipo,titulo);
}

function StatusMsj(msj) {
    window.status = msj;
}

function MessageBox(msj) {
    alert(msj);
}

function Confirmacion(msj, clientID) {
    var ctl = document.getElementById(clientID);
    if (confirm(msj))
        ctl.value = "true";
    else
        ctl.value = "false";
}

function CargarPaginaEtiqueta(){
window.open("~/Reports/EtiquetaPlegado.aspx","","Fullscreen=1");
window.focus();
}

function ImprimirEtiquetas(clientID) {
    var obj = document.getElementById(clientID);
    if (obj.value=="1") {
        obj.value = "0";
        var myWindow = window.open("../Reports/EtiquetaPlegado.aspx", "", "Height=350,width=450,modal=1");        
    }
    obj = null;        
}

function EjecutarApp(paramString) {
    var WSHShell;
    var wError;
    var enlace = "WF_PruebaEtiquetasIndusal.exe " + paramString;    
    try {
        WSHShell = new ActiveXObject("wscript.shell");
        wError = WSHShell.run(enlace);
    }
    catch (mierr) {
        alert("Error al imprimir etiqueta");
    }
    
    WSHShell = null;
    //wError = WSHShell.run(enlace, 1, False);
}

function Cerrar(){
    window.close();
}

function ConectarCanalConteo(ctlName) {
    var WSHShell;
    var wError;
    var ctl;    
    try {
        ctl = document.getElementById(ctlName);
        WSHShell = new ActiveXObject("DLL_Indusal.InterfazConteoCOM");        
        ctl.value = WSHShell.Get();
        //alert(ctl.value);
    }
    catch (mierr) {
        alert(mierr);        
    }

    WSHShell = null;
    //wError = WSHShell.run(enlace, 1, False);
}

function ConectarCanalConteoPrueba(ctlName,valor) {
    
    var ctl;
    try {
        ctl = document.getElementById(ctlName);   
        ctl.value = valor;        
    }
    catch (mierr) {
        alert(mierr);
    }    
}

function ConectarBascula(ctlName,ctlNameVer) {
    var WSHShell;
    var wError;
    var ctl;
    var ctlVer;

    try {
        ctl = document.getElementById(ctlName);
        ctlVer = document.getElementById(ctlNameVer);
        WSHShell = new ActiveXObject("DLL_Indusal.InterfazBasculas");
        ctl.value = WSHShell.GetPeso();
        ctlVer.value = ctl.value;    
        //alert(ctl.value);
    }
    catch (mierr) {
        alert(mierr);
    }

    WSHShell = null;
    //wError = WSHShell.run(enlace, 1, False);
}