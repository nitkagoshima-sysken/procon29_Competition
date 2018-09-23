using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フィールドを描画するクラスです。
    /// </summary>
    public class DrawField
    {
        /// <summary>
        /// エージェントがどこへ行きたいかを設定または取得します。
        /// </summary>
        public AgentsActivityData AgentsActivityData { get; set; }

        /// <summary>
        /// エージェントの画像を設定または取得します。
        /// </summary>
        public CharactorBitmap AgentBitmap { get; set; }

        /// <summary>
        /// エージェントの画像のレタリング時のビットマップとメタファイルの色の操作方法を設定または取得します。
        /// </summary>
        public ImageAttributes AgentImageAttributes { get; set; }

        /// <summary>
        /// 透明なエージェントの画像のレタリング時のビットマップとメタファイルの色の操作方法を設定または取得します。
        /// </summary>
        public ImageAttributes AgentTransparentImageAttributes { get; set; }

        /// <summary>
        /// 背景をどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush BackColorSolidBrush { get; set; } = new SolidBrush(Color.FromArgb(48, 48, 48));

        /// <summary>
        /// 描画するビットマップを指定します。
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// 描画する対象となる計算機を設定または取得します。
        /// </summary>
        public Calc Calc { get; set; }

        /// <summary>
        /// マスの高さを設定または取得します。
        /// </summary>
        protected int CellHeight { get; set; }

        /// <summary>
        /// マスの幅を設定または取得します。
        /// </summary>
        protected int CellWidth { get; set; }

        /// <summary>
        /// マスの枠をどのように描画するか設定または取得します。
        /// </summary>
        public Pen EdgePen { get; set; } = Pens.Black;

        /// <summary>
        /// フルーツフェアリーの画像を設定または取得します。
        /// </summary>
        public CharactorBitmap FruitFairyBitmap { get; set; }

        /// <summary>
        /// フルーツフェアリーの画像のレタリング時のビットマップとメタファイルの色の操作方法を設定または取得します。
        /// </summary>
        public ImageAttributes FruitFairyImageAttributes { get; set; }

        /// <summary>
        /// マウスオーバーしたマスをどのように描画するか設定または取得します。
        /// </summary>
        public SolidBrush MouseOverCellBrush { get; set; } = new SolidBrush(Color.FromArgb(50, Color.DarkGray));

        /// <summary>
        /// 名前を表示するフォントのファミリーネームを設定または取得します。
        /// </summary>
        public string NameFamilyName { get; set; } = "Impact";

        /// <summary>
        /// 名前を表示するフォントを設定または取得します。
        /// </summary>
        public Font NameFont { get; set; }

        /// <summary>
        /// フォントのファミリーネームを設定または取得します。
        /// </summary>
        public string PointFamilyName { get; set; } = "Impact";

        /// <summary>
        /// フォントを設定または取得します。
        /// </summary>
        public Font PointFont { get; set; }

        /// <summary>
        /// 描画するときの色を設定または取得します。
        /// </summary>
        public TeamDesigns TeamDesigns { get; set; } = new TeamDesigns();

        /// <summary>
        /// DrawFieldを初期化します。
        /// </summary>
        public DrawField()
        {
        }

        /// <summary>
        /// DrawFieldを初期化します。
        /// </summary>
        public DrawField(Calc calc, Bitmap bitmap)
        {
            Bitmap = bitmap;
            Calc = calc;

            // ResourceManagerを取得する
            System.Resources.ResourceManager resource = Properties.Resources.ResourceManager;
            // 画像ファイルを読み込んで、Imageオブジェクトとして取得する
            AgentBitmap = new CharactorBitmap(2);
            AgentBitmap[0, Direction.Rightward] = (Bitmap)resource.GetObject("Orange");
            AgentBitmap[1, Direction.Rightward] = (Bitmap)resource.GetObject("Lime");
            FruitFairyBitmap = new CharactorBitmap(4);
            FruitFairyBitmap[0, Direction.Leftward] = (Bitmap)resource.GetObject("Strawberry");
            FruitFairyBitmap[1, Direction.Leftward] = (Bitmap)resource.GetObject("Apple");
            FruitFairyBitmap[2, Direction.Leftward] = (Bitmap)resource.GetObject("Kiwi");
            FruitFairyBitmap[3, Direction.Leftward] = (Bitmap)resource.GetObject("Muscat");
            // 反転作業
            for (int i = 0; i < 2; i++)
            {
                AgentBitmap[i, Direction.Leftward] = (Bitmap)AgentBitmap[i, Direction.Rightward].Clone();
                AgentBitmap[i, Direction.Leftward].RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            for (int i = 0; i < 4; i++)
            {
                FruitFairyBitmap[i, Direction.Rightward] = (Bitmap)FruitFairyBitmap[i, Direction.Leftward].Clone();
                FruitFairyBitmap[i, Direction.Rightward].RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            ColorMatrix colorMatrix =
                new ColorMatrix
                {
                    //ColorMatrixの行列の値を変更して、アルファ値が0.8に変更されるようにする
                    Matrix00 = 1,
                    Matrix11 = 1,
                    Matrix22 = 1,
                    Matrix33 = 0.8F,
                    Matrix44 = 1
                };
            FruitFairyImageAttributes = new ImageAttributes();
            FruitFairyImageAttributes.SetColorMatrix(colorMatrix);
            colorMatrix =
                new ColorMatrix
                {
                    Matrix00 = 1,
                    Matrix11 = 1,
                    Matrix22 = 1,
                    Matrix33 = 1,
                    Matrix44 = 1
                };
            AgentImageAttributes = new ImageAttributes();
            AgentImageAttributes.SetColorMatrix(colorMatrix);
            colorMatrix =
                new ColorMatrix
                {
                    Matrix00 = 1,
                    Matrix11 = 1,
                    Matrix22 = 1,
                    Matrix33 = 0.3F,
                    Matrix44 = 1
                };
            AgentTransparentImageAttributes = new ImageAttributes();
            AgentTransparentImageAttributes.SetColorMatrix(colorMatrix);
        }

        /// <summary>
        /// 描画します。
        /// </summary>
        /// <param name="cursor">描画するマスを指定します</param>
        public void Draw(Coordinate cursor)
        {
            Ready();
            DrawBackground();
            DrawEnclosedCell();
            DrawTile();
            DrawEdge();
            DrawPoint();
            DrawFruitFairies();
            DrawMouseOverCell(cursor);
            DrawAgent(cursor);
            DrawAgentName(cursor);
        }

        /// <summary>
        /// フィールドの描画を準備します。
        /// </summary>
        protected void Ready()
        {
            CellHeight = Bitmap.Height / Calc.Field.Height;
            CellWidth = Bitmap.Width / Calc.Field.Width;
            PointFont = new Font(familyName: PointFamilyName, emSize: (CellHeight <= 0) ? 1 : CellHeight / 4 * 3 / 2.0f);
            NameFont = new Font(familyName: NameFamilyName, emSize: (CellHeight <= 0) ? 1 : CellHeight / 4 * 3 / 6.0f);
        }

        /// <summary>
        /// フィールドの背景を描画します。
        /// </summary>
        protected void DrawBackground()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            graphics.FillRectangle(
                brush: BackColorSolidBrush,
                x: 0,
                y: 0,
                width: Bitmap.Width / Calc.Field.Width * Calc.Field.Width,
                height: Bitmap.Height / Calc.Field.Height * Calc.Field.Height);
            graphics.Dispose();
        }

        /// <summary>
        /// フィールドの囲み領域を描画します。
        /// </summary>
        protected void DrawEnclosedCell()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            // 囲み領域の表示
            foreach (var cell in Calc.Field)
            {
                // 両チームともタイルを囲んでいるとき
                if (cell.IsEnclosed[Team.A] && cell.IsEnclosed[Team.B])
                {
                    graphics.FillRectangle(
                        brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesigns[Team.A].AgentColor),
                        x: cell.Coordinate.X * CellWidth,
                        y: cell.Coordinate.Y * CellHeight,
                        width: CellWidth / 2,
                        height: CellHeight);
                    graphics.FillRectangle(
                        brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesigns[Team.B].AgentColor),
                        x: cell.Coordinate.X * CellWidth + CellWidth / 2,
                        y: cell.Coordinate.Y * CellHeight,
                        width: CellWidth / 2,
                        height: CellHeight);
                }
                // チームAだけ囲んでいるとき
                else if (cell.IsEnclosed[Team.A])
                {
                    graphics.FillRectangle(
                        brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesigns[Team.A].AgentColor),
                        x: cell.Coordinate.X * CellWidth,
                        y: cell.Coordinate.Y * CellHeight,
                        width: CellWidth,
                        height: CellHeight);
                }
                // チームBだけ囲んでいるとき
                else if (cell.IsEnclosed[Team.B])
                {
                    graphics.FillRectangle(
                        brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesigns[Team.B].AgentColor),
                        x: cell.Coordinate.X * CellWidth,
                        y: cell.Coordinate.Y * CellHeight,
                        width: CellWidth,
                        height: CellHeight);
                }
            }
            graphics.Dispose();
        }

        /// <summary>
        /// タイルを描画します。
        /// </summary>
        protected void DrawTile()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (var cell in Calc.Field)
            {
                DrawTile(graphics, cell);
            }
            graphics.Dispose();
        }

        /// <summary>
        /// タイルを描画します。
        /// </summary>
        /// <param name="cell">描画するマスを指定します</param>
        public void DrawTile(Cell cell)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            DrawTile(graphics, cell);
            graphics.Dispose();
        }

        /// <summary>
        /// タイルを描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="cell">描画するマスを指定します</param>
        protected void DrawTile(Graphics graphics, Cell cell)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                if (cell.IsTileOn[team])
                    graphics.FillRectangle(
                        brush: new SolidBrush(TeamDesigns[team].AgentColor),
                        x: cell.Coordinate.X * CellWidth,
                        y: cell.Coordinate.Y * CellHeight,
                        width: CellWidth,
                        height: CellHeight);
            }
        }

        /// <summary>
        /// マスの枠を描画します。
        /// </summary>
        protected void DrawEdge()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (var cell in Calc.Field)
            {
                graphics.DrawRectangle(
                    pen: EdgePen,
                    x: cell.Coordinate.X * CellWidth,
                    y: cell.Coordinate.Y * CellHeight,
                    width: CellWidth,
                    height: CellHeight);
            }
            graphics.Dispose();
        }

        /// <summary>
        /// マスの枠を描画します。
        /// </summary>
        /// <param name="cell">描画するマスを指定します</param>
        public void DrawEdge(Cell cell)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            DrawEdge(graphics, cell);
            graphics.Dispose();

        }

        /// <summary>
        /// マスの枠を描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="cell">描画するマスを指定します</param>
        protected void DrawEdge(Graphics graphics, Cell cell)
        {
            graphics.DrawRectangle(
                    pen: EdgePen,
                    x: cell.Coordinate.X * CellWidth,
                    y: cell.Coordinate.Y * CellHeight,
                    width: CellWidth,
                    height: CellHeight);
        }

        /// <summary>
        /// マスの得点を描画します。
        /// </summary>
        protected void DrawPoint()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (var cell in Calc.Field)
            {
                DrawPoint(graphics, cell);
            }
            graphics.Dispose();
        }

        /// <summary>
        /// マスの得点を描画します。
        /// </summary>
        /// <param name="cell">描画するマスを指定します</param>
        public void DrawPoint(Cell cell)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            DrawPoint(graphics, cell);
            graphics.Dispose();
        }

        /// <summary>
        /// マスの枠を描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="cell">描画するマスを指定します</param>
        protected void DrawPoint(Graphics graphics, Cell cell)
        {
            graphics.DrawString(
                    s: ((0 <= cell.Point && cell.Point <= 9) ? " " : string.Empty) + cell.Point.ToString(),
                    font: PointFont,
                    brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                    x: (float)(cell.Coordinate.X + ((-10 <= cell.Point) ? 0.1 : 0.0)) * CellWidth,
                    y: (float)(cell.Coordinate.Y + 0.1) * CellHeight);
        }

        /// <summary>
        /// フルーツフェアリーたちを描画します。
        /// </summary>
        protected void DrawFruitFairies()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                {
                    float f = Bitmap.Height / 12000.0f;

                    var bmp = (AgentsActivityData[team, agent].Destination.X < Calc.Field.Width / 2)
                        ? FruitFairyBitmap[(int)team * 2 + (int)agent, Direction.Rightward]
                        : FruitFairyBitmap[(int)team * 2 + (int)agent, Direction.Leftward];

                    graphics.DrawImage(
                        image: bmp,
                        destRect: new Rectangle
                        {
                            X = (int)((AgentsActivityData[team, agent].Destination.X + 0.5f) * CellWidth),
                            Y = (int)((AgentsActivityData[team, agent].Destination.Y + 0.0f) * CellHeight),
                            Width = (int)(bmp.Width * f),
                            Height = (int)(bmp.Height * f)
                        },
                        srcX: 0,
                        srcY: 0,
                        srcWidth: bmp.Width,
                        srcHeight: bmp.Height,
                        srcUnit: GraphicsUnit.Pixel,
                        imageAttrs: FruitFairyImageAttributes);
                }
            }
            graphics.Dispose();
        }

        /// <summary>
        /// マウスオーバーしたマスを描画します。
        /// </summary>
        /// <param name="cursor">カーソルの座標を指定します。</param>
        protected void DrawMouseOverCell(Coordinate cursor)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            var mouseOverCell = new Coordinate(
                x: cursor.X,
                y: cursor.Y);
            if ((mouseOverCell.X < Calc.Field.Width) && (mouseOverCell.Y < Calc.Field.Height))
                graphics.FillRectangle(
                    brush: MouseOverCellBrush,
                    x: mouseOverCell.X * CellWidth,
                    y: mouseOverCell.Y * CellHeight,
                    width: CellWidth,
                    height: CellHeight);
        }

        /// <summary>
        /// エージェントを表示します。
        /// </summary>
        /// <param name="cursor">カーソルの座標を指定します。</param>
        protected void DrawAgent(Coordinate cursor)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            // 上から順に描画するためにリスト化してソートする。
            List<Agent> list = new List<Agent>();
            foreach (var item in Calc.Agents)
            {
                list.Add(item);
            }
            list.Sort((a, b) => a.Position.Y - b.Position.Y);
            // エージェントを女の子にするところ
            foreach (var agent in list)
            {
                DrawAgent(graphics, agent, cursor);
            }
            graphics.Dispose();
        }

        /// <summary>
        /// エージェントたちを描画します。
        /// </summary>
        /// <param name="agent">描画するエージェントを指定します</param>
        /// <param name="cursor">カーソルの座標を指定します。</param>
        public void DrawAgent(Agent agent, Coordinate cursor)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            DrawAgent(graphics, agent, cursor);
            graphics.Dispose();
        }

        /// <summary>
        /// エージェントたちを描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="agent">描画するエージェントを指定します</param>
        /// <param name="cursor">カーソルの座標を指定します。</param>
        protected void DrawAgent(Graphics graphics, Agent agent, Coordinate cursor)
        {
            // 司令塔の邪魔にならないように、エージェントの真上のマスをマウスが通ったときに、
            // フルーツフェアリーたちの魔法で透明になるという設定
            ImageAttributes imageAttributes;
            if (cursor.X == Calc.Agents[agent.Team, agent.AgentNumber].Position.X && (
                cursor.Y == Calc.Agents[agent.Team, agent.AgentNumber].Position.Y - 1 ||
                cursor.Y == Calc.Agents[agent.Team, agent.AgentNumber].Position.Y))
            {
                imageAttributes = AgentTransparentImageAttributes;
            }
            else
            {
                imageAttributes = AgentImageAttributes;
            }
            DrawAgent(graphics, agent, imageAttributes);
        }

        /// <summary>
        /// エージェントたちを描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="agent">描画するエージェントを指定します</param>
        protected void DrawAgent(Graphics graphics, Agent agent)
        {
            ImageAttributes imageAttributes;
            imageAttributes = AgentImageAttributes;
            DrawAgent(graphics, agent, imageAttributes);
        }

        /// <summary>
        /// エージェントたちを描画します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="agent">描画するエージェントを指定します</param>
        /// <param name="imageAttributes"></param>
        protected void DrawAgent(Graphics graphics, Agent agent, ImageAttributes imageAttributes)
        {
            float f = Bitmap.Height / 3000.0f;
            var bmp = (Calc.Agents[agent.Team, agent.AgentNumber].Position.X > Calc.Field.Width / 2)
                ? AgentBitmap[(int)agent.Team, Direction.Rightward]
                : AgentBitmap[(int)agent.Team, Direction.Leftward];
            graphics.DrawImage(
                image: bmp,
                destRect: new Rectangle
                {
                    X = (int)(Calc.Agents[agent.Team, agent.AgentNumber].Position.X * CellWidth),
                    Y = (int)(Calc.Agents[agent.Team, agent.AgentNumber].Position.Y * CellHeight - (bmp.Height * f * 0.55f)),
                    Width = (int)(bmp.Width * f),
                    Height = (int)(bmp.Height * f)
                },
                srcX: 0,
                srcY: 0,
                srcWidth: bmp.Width,
                srcHeight: bmp.Height,
                srcUnit: GraphicsUnit.Pixel,
                imageAttrs: imageAttributes);
        }

        /// <summary>
        /// 短い名前を表示します。
        /// </summary>
        /// <param name="cursor">カーソルの座標を指定します。</param>
        protected void DrawAgentName(Coordinate cursor)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (var agent in Calc.Agents)
            {
                if (!(cursor.X == agent.Position.X &&
                    cursor.Y == agent.Position.Y - 1))
                {
                    DrawAgentName(graphics, agent);
                }
            }
            graphics.Dispose();
        }

        /// <summary>
        /// 短い名前を表示します。
        /// </summary>
        protected void DrawAgentName()
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            foreach (var agent in Calc.Agents)
            {
                DrawAgentName(graphics, agent);
            }
            graphics.Dispose();
        }

        /// <summary>
        /// 短い名前を表示します。
        /// </summary>
        /// <param name="agent">描画するエージェントを指定します</param>
        protected void DrawAgentName(Agent agent)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            DrawAgentName(graphics, agent);
            graphics.Dispose();
        }

        /// <summary>
        /// 短い名前を表示します。
        /// </summary>
        /// <param name="graphics">描画サーフェスを指定します</param>
        /// <param name="agent">描画するエージェントを指定します</param>
        protected void DrawAgentName(Graphics graphics, Agent agent)
        {
            graphics.DrawString(
                    s: agent.Name,
                    font: NameFont,
                    brush: new SolidBrush(color: Color.FromArgb(0xCC, Color.White)),
                    x: (float)(agent.Position.X + 0.0) * CellWidth,
                    y: (float)(agent.Position.Y + 0.75) * CellHeight);
        }

        /// <summary>
        /// エージェントが移動する矢印を描画します。
        /// </summary>
        public void DrawArrow(Team team, Coordinate from, Coordinate to)
        {
            Graphics graphics = Graphics.FromImage(Bitmap);
            Pen pen = new Pen(Color.Red, 8);
            pen.DashStyle = DashStyle.Dot;
            pen.EndCap = LineCap.ArrowAnchor;
            graphics.DrawLine(
                pen,
                (int)((from.X + 0.5) * CellWidth),
                (int)((from.Y + 0.5) * CellHeight),
                (int)((to.X + 0.5) * CellWidth),
                (int)((to.Y + 0.5) * CellHeight));
        }
    }
}
