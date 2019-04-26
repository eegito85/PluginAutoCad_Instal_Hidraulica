using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using FazHidraulicaCAD.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazHidraulicaCAD.Funcoes
{
    public class ComandosCAD
    {
        List<Elemento> listaElementos = new List<Elemento>();
        LeituraPastasArquivos arquivos = new LeituraPastasArquivos();

        public List<LinhaComAtributo> SelecionarDuto()
        {
            List<LinhaComAtributo> listaDeDutos = new List<LinhaComAtributo>();
            double comprimento = 0;

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor acDocEd = acDoc.Editor;

            //COMEÇAR UMA TRANSAÇÃO
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                TypedValue[] filList = new TypedValue[1] { new TypedValue((int)DxfCode.Start, "LWPOLYLINE") };
                SelectionFilter filter = new SelectionFilter(filList);
                PromptSelectionOptions opts = new PromptSelectionOptions();
                opts.MessageForAdding = "Selecione a tubulação: ";
                PromptSelectionResult res = acDocEd.GetSelection(opts, filter);

                if (res.Status == PromptStatus.OK)
                {
                    SelectionSet selSet = res.Value;
                    ObjectId[] ids = selSet.GetObjectIds();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Polyline pl = (Polyline)acTrans.GetObject(ids[i], OpenMode.ForRead);
                        LinhaComAtributo duto = new LinhaComAtributo();
                        duto.ID = "";
                        duto.Nome = pl.Layer;
                        duto.Descricao = "Tubulação hidráulica";
                        comprimento = Math.Round(pl.Length, 3);
                        duto.Comprimento = comprimento.ToString();
                        listaDeDutos.Add(duto);
                    }
                }
                acTrans.Commit();
            }
            return listaDeDutos;
        }

        public void InserirBlocoNoDesenho(string nomeArquivo)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database currDwgDb = acDoc.Database;
            Editor acDocEd = acDoc.Editor;
            try
            {
                using (DocumentLock docLock = acDoc.LockDocument())
                {
                    using (Database xDb = new Database(false, true))
                    {
                        xDb.ReadDwgFile(nomeArquivo, System.IO.FileShare.Read, true, "");
                        using (Transaction tr = acDoc.TransactionManager.StartTransaction())
                        {
                            string name = SymbolUtilityServices.GetBlockNameFromInsertPathName(nomeArquivo);
                            ObjectId id = currDwgDb.Insert(name, xDb, true);

                            if (id.IsNull)
                            {
                                acDocEd.WriteMessage("Failed to insert block");
                                return;
                            }
                            BlockTableRecord currSpace = (BlockTableRecord)(tr.GetObject(currDwgDb.CurrentSpaceId, OpenMode.ForWrite));
                            PromptPointResult pPtRes;
                            PromptPointOptions pPtOpts = new PromptPointOptions("");
                            //Escolher o ponto de inserção
                            pPtOpts.Message = "\nEscolha o ponto de inserção do insumo: ";
                            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
                            Point3d p3d = pPtRes.Value;
                            CoordinateSystem3d coordS = new CoordinateSystem3d(p3d, currDwgDb.Ucsxdir, currDwgDb.Ucsydir);
                            BlockReference insert = new BlockReference(p3d, id);
                            insert.Normal = coordS.Zaxis;

                            //Dictionary<string, string> attValues = new Dictionary<string, string>();
                            //attValues.Add("ATT1", "foo");
                            //
                            //RXClass attDefClass = RXClass.GetClass(typeof(AttributeDefinition));
                            //foreach (ObjectId oid in currSpace)
                            //{
                            //    if (id.ObjectClass != attDefClass)
                            //        continue;
                            //    AttributeDefinition attDef = (AttributeDefinition)tr.GetObject(oid, OpenMode.ForRead);
                            //    AttributeReference attRef = new AttributeReference();
                            //    attRef.SetAttributeFromBlock(attDef, insert.BlockTransform);
                            //    if (attValues != null && attValues.ContainsKey(attDef.Tag.ToUpper()))
                            //    {
                            //        attRef.TextString = attValues[attDef.Tag.ToUpper()];
                            //    }
                            //    insert.AttributeCollection.AppendAttribute(attRef);
                            //    tr.AddNewlyCreatedDBObject(attRef, true);
                            //}

                            currSpace.AppendEntity(insert);
                            insert.SetDatabaseDefaults();
                            tr.AddNewlyCreatedDBObject(insert, true);
                            tr.Commit();
                        }
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                acDocEd.WriteMessage("\nError during copy: " + ex.Message);
            }
        }

        //SELECIONAR UMA REGIÃO DO PROJETO PARA LEVANTAR O QUANTITATIVO
        public List<Elemento> SelecionarBlocos()
        {
            List<double> contagem = new List<double>();
            int Id = 0;
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor acDocEd = acDoc.Editor;
            List<string> listaDeAtributos = new List<string>();

            //COMEÇAR UMA TRANSAÇÃO
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                //CRIAR UM ARRAY COM O(S) TIPO(S) DE OBJETO(S) A SER(EM) FILTRADO(S)
                TypedValue[] acTypValAr = new TypedValue[1];
                //acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "CIRCLE"), 0);
                acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);

                //CRIAR O FILTRO COM O ARRAY DECLARADO ACIMA
                SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

                //É PEDIDO AO USUÁRIO SELECIONAR UMA REGIÃO DO DESENHO
                PromptSelectionResult acSSPrompt = acDocEd.GetSelection(acSelFtr);

                string tipo = "";
                string id = "";
                string nome = "";
                
                string quant = "0";

                //CASO ELE SELECIONE UMA REGIÃO
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    //PERCORRE OS OBJETOS DO CONJUNTO DE SELEÇÃO
                    foreach (SelectedObject acSSObj in acSSet)
                    {
                        Elemento elemento = new Elemento();
                        //VERIFICAR SE O OBJETO SELECIONADO EXISTE
                        if (acSSObj != null)
                        {
                            // Open the selected object for write
                            Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as Entity;
                            contagem.Add(10);

                            if (acEnt.GetType() == typeof(BlockReference))
                            {
                                Id++;
                                id = Convert.ToString(Id);
                                BlockReference blRef = acEnt as BlockReference;
                                nome = blRef.Name;
                                elemento.Nome = nome;
                                AttributeCollection atributos = blRef.AttributeCollection;
                                int qtde = atributos.Count; //----->>>>>>>>>>  quantidade de atributos no bloco
                                //MessageBox.Show(qtde.ToString(), "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (qtde > 0)
                                {
                                    foreach(ObjectId AttId in atributos)
                                    {
                                        AttributeReference attref = (AttributeReference)acTrans.GetObject(AttId, OpenMode.ForRead);
                                        if (attref != null)
                                        {
                                            listaDeAtributos.Add(attref.TextString);
                                            //MessageBox.Show(attref.TextString, "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            elemento.Especificacao = attref.TextString;
                                        }
                                    }
                                    //elemento.Especificacao = MontaEspecificacaoBloco(listaDeAtributos);
                                }
                                if (qtde <= 0)
                                {
                                    elemento.Especificacao = "Peça sem atributo associado";
                                }
                                elemento.Tipo = "bloco";
                                elemento.Quantificacao = quant;
                                elemento.ID = id;
                                //tipo = RetornaTipoElemento(nome);
                                //MessageBox.Show("Nome do bloco: " + nome, "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Now you can open BlockTableRecord:
                                //BlockTableRecord acBlkTblRec = acTrans.GetObject(blRef.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                                //foreach (ObjectId eid in acBlkTblRec)
                                //{
                                //    DBObject obj = (Entity)acTrans.GetObject(eid, OpenMode.ForRead);
                                //
                                //    if (obj is AttributeDefinition)
                                //    {
                                //        AttributeDefinition atdef = obj as AttributeDefinition;
                                //        if (atdef.Constant)
                                //        {
                                //            Id++;
                                //            id = Convert.ToString(Id);
                                //            especificacao = atdef.Tag;
                                //            elemento.ID = id;
                                //            elemento.Especificacao = especificacao;
                                //            elemento.Quantificacao = quant;
                                //            MessageBox.Show(atdef.Tag, "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            MessageBox.Show(atdef.TextString, "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //        }
                                //    }
                                //
                                //}
                                //acBlkTblRec.Dispose();
                                //AttributeCollection attCol = blRef.AttributeCollection;
                                //foreach (ObjectId attId in attCol)
                                //{
                                //    AttributeReference attRef = (AttributeReference)acTrans.GetObject(attId, OpenMode.ForRead);
                                //    string str = ("\n  Attribute Tag: " + attRef.Tag + "\n    Attribute String: " + attRef.TextString);
                                //    MessageBox.Show(str, "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                            }

                        }
                        if (elemento.Nome != "") { listaElementos.Add(elemento); }

                    }
                    //SALVAR O OBJETO NO BANCO DE DADOS
                    acTrans.Commit();
                }
                //int qtd = contagem.Count();
                //MessageBox.Show("Número de blocos selecionados: " + qtd.ToString(), "Contagem de blocos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Dispose of the transaction
            }
            return listaElementos;
        }

        private string RetornaTipoElemento(string nomeElemento)
        {
            string tipo = "";
            nomeElemento = nomeElemento.ToUpper();
            string[] palavras = nomeElemento.Split(' ');
            string primeiraPalavra = palavras[0];
            if (primeiraPalavra == "ELETRODUTO" || primeiraPalavra == "TUBULAÇÃO" || primeiraPalavra == "TUBULACAO" || primeiraPalavra == "ELETRODUTO" || primeiraPalavra == "CORDOALHA" || primeiraPalavra == "DUTO")
            {
                tipo = "duto";
            }
            else { tipo = "bloco"; }
            return tipo;
        }

        private string MontaEspecificacaoBloco(List<string> listaAtributos)
        {
            string especificacao = "";
            int n = listaAtributos.Count;
            for (int i = 0; i < n; i++)
            {
                especificacao = especificacao + " " + listaAtributos[i];
            }

            return especificacao;
        }


    }
}
