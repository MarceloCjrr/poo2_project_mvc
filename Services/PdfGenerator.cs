using iTextSharp.text;
using iTextSharp.text.pdf;
using poo2_poject_mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace poo2_poject_mvc.Services
{
    public static class PdfGenerator
    {
        public static void GerarRelatorioProdutos(List<Produto> produtos)
        {
            // 1. Configurações Iniciais
            Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "RelatorioProdutos.pdf");

            try
            {
                // 2. Cria o escritor PDF
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoArquivo, FileMode.Create));
                doc.Open();

                // Configuração de Fontes (Para suportar caracteres especiais)
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fonteTitulo = new Font(baseFont, 16, Font.BOLD);
                Font fonteTexto = new Font(baseFont, 10, Font.NORMAL);
                Font fonteCabecalho = new Font(baseFont, 10, Font.BOLD, BaseColor.WHITE);

                // 3. Adiciona Título
                Paragraph titulo = new Paragraph("Relatório de Produtos e Categorias", fonteTitulo);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 20f;
                doc.Add(titulo);

                // 4. Cria a Tabela (5 Colunas)
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100; // Ocupa toda a largura
                table.SetWidths(new float[] { 0.5f, 3f, 1f, 1.5f }); // Proporção das colunas

                // 5. Adiciona Cabeçalho da Tabela
                string[] cabecalhos = { "ID", "Nome do Produto", "Preço", "Categoria" };
                foreach (var header in cabecalhos)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, fonteCabecalho));
                    cell.BackgroundColor = new BaseColor(60, 110, 180); // Cor de fundo azul
                    cell.Padding = 8f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    table.AddCell(cell);
                }

                // 6. Preenche a Tabela com Dados
                foreach (var produto in produtos)
                {
                    // ID
                    PdfPCell cellId = new PdfPCell(new Phrase(produto.Id.ToString(), fonteTexto));
                    cellId.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cellId);

                    // Nome
                    table.AddCell(new PdfPCell(new Phrase(produto.Nome, fonteTexto)));

                    // Preço (Formato de Moeda)
                    PdfPCell cellPreco = new PdfPCell(new Phrase(produto.Preco.ToString("C"), fonteTexto));
                    cellPreco.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cellPreco);

                    // Categoria
                    table.AddCell(new PdfPCell(new Phrase(produto.Categoria?.Nome ?? "N/A", fonteTexto)));
                }

                // 7. Adiciona a Tabela ao Documento
                doc.Add(table);

                // 8. Mensagem de rodapé
                Paragraph rodape = new Paragraph($"Total de Registros: {produtos.Count}", fonteTexto);
                rodape.Alignment = Element.ALIGN_LEFT;
                rodape.SpacingBefore = 20f;
                doc.Add(rodape);

                doc.Close();

                // Abre o arquivo PDF no visualizador padrão do sistema
                System.Diagnostics.Process.Start(caminhoArquivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar PDF. Certifique-se de que o arquivo não está aberto.\nDetalhe: " + ex.Message, "Erro de Geração de PDF");
            }
            finally
            {
                if (doc != null && doc.IsOpen())
                {
                    doc.Close();
                }
            }
        }
    }
}
