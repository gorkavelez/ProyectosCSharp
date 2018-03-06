function StatusMsj(msj) {
    window.status = msj;
}

function MessageBox(msj) {
    alert(msj);
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

function FocusScript(clientID) {
    document.getElementById(clientID).focus();
    document.getElementById(clientID).select();
}

function ventanaSecundaria(URL) {
    window.open(URL, "ventana1", "width=700,height=700,scrollbars=YES,resizAble=YES")
}