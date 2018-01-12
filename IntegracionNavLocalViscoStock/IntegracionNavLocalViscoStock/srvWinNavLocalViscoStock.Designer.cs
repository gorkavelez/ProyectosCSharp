namespace IntegracionNavLocalViscoStock
{
    partial class srvWinNavLocalViscoStock
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.logSrvIntNavLocalViscoStock = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.logSrvIntNavLocalViscoStock)).BeginInit();
            // 
            // srvWinNavLocalViscoStock
            this.logSrvIntNavLocalViscoStock.Log = "Application";
            this.logSrvIntNavLocalViscoStock.Source = "ViscoStock Integration";
            this.logSrvIntNavLocalViscoStock.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.logSrvIntNavLocalViscoStock_EntryWritten);
            // 
            // Integracion
            this.ServiceName = "IntegracionViscoStock";
            ((System.ComponentModel.ISupportInitialize)(this.logSrvIntNavLocalViscoStock)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog logSrvIntNavLocalViscoStock;
    }
}
