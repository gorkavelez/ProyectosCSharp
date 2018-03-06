
function FocusScript(clientID) {

    document.getElementById(clientID).focus();
    document.getElementById(clientID).select();
   
}

function FocusScriptWithAction(clientID, tipo, titulo) {

    FocusScript(clientID);
    __doPostBack(tipo, titulo);
    
}
