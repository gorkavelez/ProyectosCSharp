function ConectarCanalConteo(ctlName) {
    var WSHShell;
    var wError;
    var ctl;
    try {
        ctl = document.getElementById(ctlName);
        WSHShell = new ActiveXObject("DLL_Indusal.InterfazConteoCOM");        
        ctl.value = WSHShell.Get();
        alert(ctl.value);
    }
    catch (mierr) {
        alert("Error Javascript " + mierr);
    }

    WSHShell = null;
    //wError = WSHShell.run(enlace, 1, False);
}