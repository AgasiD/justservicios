using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public class excelControllers
    {
        string precio;
        int cantColumnas = 3;
        List<Columna> columnas = new List<Columna>();
        private static excelControllers instancia;

        public excelControllers()
        {

        }
        public static excelControllers getIntancia()
        {
            if(instancia == null)
            {
                return new excelControllers();
            }
            return instancia;
        }

        public XLWorkbook ListadoPrecios(List<MiLPrecios> lista, int cantidadColumnas)
        {
            cantColumnas = 3;// cantidadColumnas;
            const int  CINICIO = 3, FINICIO = 3;
            int fila, columna;
         /*   try
            {*/
            //obtengo los codigo de rubros que existan en la lista
                var codRubros = lista.GroupBy(art => art.codRubro).Select(rub => new { codrub = rub.Key}).OrderBy(a => a.codrub).ToArray();
                List<Rubro> rubros;
                List<Subrubro> subrubs;
                string queryRubros = "";
            //genero la query para traer de la base todos los RUBROS en la lista
            for(int a = 0; a < codRubros.Length; a++)
            {
                queryRubros += codRubros[a].codrub + ",";
            }
            queryRubros = queryRubros.Substring(0, queryRubros.Length - 1);
            using(GestionEntities bd = new GestionEntities())
            {
                rubros = bd.Database.SqlQuery<Rubro>("select codigo, descri from rubros where codigo in (" + queryRubros+ ")").ToList();  
            }
                
            var workbook = new XLWorkbook();
            for (int b = 0; b < rubros.Count; b++) {
                //por cada rubro, crea una hoja por cada uno
                char[] quitChar = { '/', '?', '*', '[', ']', '\''};
                rubros[b].descri = rubros[b].descri.Replace('/', ' ');
                var worksheet = workbook.Worksheets.Add(rubros[b].descri);
                fila = FINICIO;
                columna = CINICIO;
                var celda = worksheet.Cell(fila, columna);
                //obtengo todos los codigos de subrubros de los rubros en los articulos de la lista
                var codSubrubs = lista.Where(art => art.codRubro == codRubros[b].codrub).GroupBy(art => art.codSubrub)
                    .Select(sub => new { codSubRub = sub.Key }).OrderBy(a => a.codSubRub).ToList();
                string querySubRub = "";
                
                //busco los subrubros
                using (GestionEntities bd = new GestionEntities())
                {
                    subrubs = bd.Database.SqlQuery<Subrubro>("select codigo, descri from subrub").ToList();
                }
                //por cada subrubro se insertan los articulos pertenecientes
                string descriSub;
                for (int d = 0;  d < codSubrubs.Count; d++)
                {
                    descriSub = getDescriSubrub(codSubrubs[d].codSubRub, subrubs);
                        // var subrubro = subrubs.Where(sub => codSubrubs[x].codSubRub == sub.codigo).Single().descri;
                    //obtiene todos los articulos que tengan mismo idRUBRO y idSUBRUBRO
                    var listaArticulos = lista.Where(art => art.codRubro == rubros[b].codigo && art.codSubrub == codSubrubs[d].codSubRub).ToList();
                    celda = insertaCabeceraSubRubro(celda, descriSub,worksheet);
                    foreach (var art in listaArticulos) {
                       // for (int e = 0; e <= cantColumnas; e++)
                       // {
                            celda = insertaRegistro(celda, art, worksheet);
                            //setWithToCell(celda, worksheet);
                       // }
                    }
                    fila = FINICIO;
                    columna+= 5;
                    celda = worksheet.Cell(fila, columna);
                }
            }
                    //workbook.SaveAs("C:/Users/usuario/Desktop/exeles/ListadoPrecio/Precios" + DateTime.Now.Date.ToString("ddMMyyyy") + ".xlsx");
                    return workbook;
               }
         /*   }   
            catch (Exception)
            {
                return null;
            }*/
            

            private void setWithToCell(IXLCell celda)
        {
            //da tamaño a la celdas;
          
            
            
        }

        private string getDescriSubrub(decimal codigo, List<Subrubro> subs)
        {
            return subs.Where(sub => sub.codigo == codigo).Single().descri;
        }

        private IXLCell insertaRegistro(IXLCell celda, MiLPrecios art, IXLWorksheet ws)
        {
            celda.Value = art.codpro;
            ws.Column(celda.Address.ColumnLetter).AdjustToContents();
            celda.CellRight().Value = art.descri;
            ws.Column(celda.CellRight().Address.ColumnNumber).AdjustToContents();
            celda.CellRight().CellRight().Value = art.simbolo;
            ws.Column(celda.CellRight().CellRight().Address.ColumnNumber).AdjustToContents();
            celda.CellRight().CellRight().CellRight().Value = art.moneda + art.precio;
            ws.Column(celda.CellRight().CellRight().CellRight().Address.ColumnNumber).AdjustToContents();

            /*set color to cell
            /*set text's position on cell */
            //celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            celda.CellRight().CellRight().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            /* set border to cells*/
            //primer celda
            celda.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            celda.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //segunda celda
            celda.CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //tercer celda 
            celda.CellRight().CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //cuarta celda
            celda.CellRight().CellRight().CellRight().Style.Border.RightBorder= XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;

           
            return celda.CellBelow();
        }
          

        private IXLCell insertaCabeceraSubRubro(IXLCell celda, string subrub,IXLWorksheet worksheet)
        {
            celda.Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Range(celda.Address.RowNumber, celda.Address.ColumnNumber, celda.CellRight().CellRight().CellRight().Address.RowNumber, celda.CellRight().CellRight().CellRight().Address.ColumnNumber).Merge();
            celda.Value = subrub;
            //Set border to cells
            celda.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
            celda.Style.Border.TopBorder = XLBorderStyleValues.Thick;
            celda.Style.Border.RightBorder = XLBorderStyleValues.Thick;
            celda.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            celda.CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            celda.CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thick;
            celda.CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            celda.CellRight().CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thick;
            celda.CellRight().CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            celda.CellRight().CellRight().CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thick;
            celda.CellRight().CellRight().CellRight().Style.Border.RightBorder = XLBorderStyleValues.Thick;
            celda.Style.Font.Bold = true;
            celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 

            celda = celda.CellBelow();
            //da valor a las celdas
            celda.Value = "CÓDIGO";
            celda.CellRight().Value = "DESCRIPCIÓN";
            celda.CellRight().CellRight().Value = "UNIDAD MED.";
            celda.CellRight().CellRight().CellRight().Value = "PRECIO";
            //SET TEXT'S POSITION ON CELL
            celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            celda.CellRight().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            celda.CellRight().CellRight().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            celda.CellRight().CellRight().CellRight().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //SET BOLD FORMAT
            celda.Style.Font.Bold = true;
            celda.CellRight().Style.Font.Bold = true;
            celda.CellRight().CellRight().Style.Font.Bold = true;
            celda.CellRight().CellRight().CellRight().Style.Font.Bold = true;
            //SET COLORS TO CELL
            celda.Style.Fill.BackgroundColor = XLColor.AliceBlue;
            celda.CellRight().Style.Fill.BackgroundColor = XLColor.AliceBlue;
            celda.CellRight().CellRight().Style.Fill.BackgroundColor = XLColor.AliceBlue;
            celda.CellRight().CellRight().CellRight().Style.Fill.BackgroundColor = XLColor.AliceBlue;
            //SET BORDER TO CELL
            celda.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            celda.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            celda.CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().CellRight().Style.Border.TopBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().CellRight().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            celda.CellRight().CellRight().CellRight().Style.Border.RightBorder = XLBorderStyleValues.Thin;
            return celda.CellBelow();
        }
    }   

    public class Columna
    {
        public Columna(string cabe, string fila)
        {
            inicioCabecera = cabe;
            inicioColumna = fila;
        }
       public string inicioCabecera { get; set; }
        public string inicioColumna { get; set; }
    }

    public class Rubro
    {
        public int codigo { get; set; }
        public string descri{ get; set; }

    }
    public class Subrubro
    {
        public decimal codigo { get; set; }
        public string descri { get; set; }

    }

    /*
    private void insertarFila(IXLCell celda, List<LPrecios> lista, string indexPrecio, MiLPrecios precio, IXLWorksheet worksheet)
    {
        celda.Value = precio.codpro;
        celda.WorksheetColumn().Width = 15;
        celda = celda.CellRight(); //cambia una fila para la derecha
        celda.WorksheetColumn().Width = 60;
        celda.Value = precio.descri;
        celda = celda.CellRight();//cambia una fila para la derecha
        celda.Value = precio.simbolo;
        celda = celda.CellRight();//cambia una fila para la derecha
        celda.Value = seleccionarPrecio(indexPrecio, precio);
        celda = celda.CellRight();//cambia una fila para la derecha
        celda = worksheet.Cell(celda.WorksheetRow().RowNumber(), 1).CellBelow(); // va a la primer celda de la fila y baja una fila
    }
    private IXLCell generarEncabezado(IXLWorksheet worksheet, Columna columna, int cantcolumnas)
    {
        var cabecera = worksheet.Cell("B1");
        cabecera.Value = "precios";// cantColumnas.ToString();
        cabecera.Style.Font.FontSize = 20;
        cabecera.Style.Font.Bold = true;
        var celda = worksheet.Cell(2, Convert.ToInt32(columna.inicioCabecera));
        List<string> listaEncabezados = new List<string>();
        listaEncabezados.Add("Código");
        listaEncabezados.Add("Descripción");
        listaEncabezados.Add("UM");
        listaEncabezados.Add("Precio");
        foreach (var cabe in listaEncabezados)
        {
            celda.Value = cabe;
            celda.Style.Font.Bold = true;
            celda = celda.CellRight();//cambia una fila para la derecha
        }
        return worksheet.FirstCellUsed().CellBelow();
    }*/
}