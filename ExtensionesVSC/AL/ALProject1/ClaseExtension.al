// Welcome to your new AL extension.
// Remember that object names and IDs should be unique across all extensions.
// AL snippets start with t*, like tpageext - give them a try and happy coding!

pageextension 50100 CustomerListExt extends "Customer List"
{
    trigger OnOpenPage();
    begin
        Message('Abrir lista clientes');
    end;  
}



pageextension 50101 CustomerExt extends "Customer Card"
{
    trigger OnOpenPage();
    begin
        Message('abrir ficha clientes');
    end;

    trigger OnAfterGetRecord()
    begin
        "Phone No.":='62000001';        
    end;  

}

