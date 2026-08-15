namespace VeluriyamLibHjson.Image;

internal class ImageTheTooltip : GlobalItem
{
    // 匹配的正则表达式
    private static Regex MatchingBuff = new Regex(@"\[vbuff\/([^\]]+)\]");
    private static Regex MatchingDebuff = new Regex(@"\[vdebuff\/([^\]]+)\]");
    // 匹配中间具体Buff的来源和名称的正则表达式
    private static Regex MatchingSpecificBuff = new Regex(@"([^\/]+)\/([^\/]+)");

    public override bool InstancePerEntity => true;

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (HjsonConfig.Instance.ImageTooltipBuff)
        {
            // 初始时候清空防止内存泄漏
            buffs.Clear();

            for (int i = 0; i < tooltips.Count; i++)
            {
                // 提前声明一下一会儿要存储的数据
                Texture2D texture = default;
                string name = default;
                string description = default;
                float length = default;

                #region 匹配并替换的局部方法
                void Match(Regex regex, string color)
                {
                    // 修改Buff文本和add实例喵
                    while (regex.Match(tooltips[i].Text).Success)
                    {
                        // 先替换最外层，只替换一个
                        tooltips[i].Text = regex.Replace(tooltips[i].Text, match =>
                        {
                            // 替换里面一层
                            return MatchingSpecificBuff.Replace(match.Groups[1].Value, key =>
                            {
                                // 获取在这之前的文本长度，方便定位buff贴图要绘制在哪里
                                length = ChatManager.GetStringSize(FontAssets.MouseText.Value, tooltips[i].Text.Substring(0, match.Index), Vector2.One).X;
                                // 如过是原版的Buff
                                if (key.Groups[1].Value == "Terraria")
                                {
                                    // 并且能获取到type
                                    if (BuffID.Search.TryGetId(key.Groups[2].Value, out int buffType))
                                    {
                                        // 直接就是添加进绘制列表好吧不带犹豫的
                                        texture = TextureAssets.Buff[buffType].Value;
                                        name = Lang.GetBuffName(buffType);
                                        description = Lang.GetBuffDescription(buffType);

                                        buffs.Add((length, i, color, texture, name, description));
                                    }
                                }
                                else
                                {
                                    // 要不然就是Mod的Buff
                                    if (ModLoader.TryGetMod(key.Groups[1].Value, out Mod source))
                                    {
                                        // 直接获取对应的ModBuff实例好吧
                                        if (source.TryFind<ModBuff>(key.Groups[2].Value, out ModBuff modbuff))
                                        {
                                            // 也是直接就是添加进绘制列表好吧不带犹豫的
                                            texture = ModContent.Request<Texture2D>(modbuff.Texture).Value;
                                            name = modbuff.DisplayName.Value;
                                            description = modbuff.Description.Value;

                                            buffs.Add((length, i, color, texture, name, description));
                                        }
                                    }
                                }

                                // 空格是给Buff贴图留的空间
                                return $"      [c/{color}:{name}]";
                            });
                        }, 1);
                    }
                }
                #endregion

                // 调用一下
                Match(MatchingBuff, "90EE90");
                Match(MatchingDebuff, "EE9090");
            }

            // 显示Buff的description的方法好吧
            if (Main.keyState.IsKeyDown(Keys.LeftControl) && buffs.Count != 0)
            {
                // 确保物品描述大于等于2
                if (tooltips.Count >= 2)
                {
                    // 移除除了物品名字以外的
                    tooltips.RemoveRange(1, tooltips.Count - 1);

                    // 遍历一下都添加进去了什么要绘制的Buff
                    for (int j = 0; j < buffs.Count; j++)
                    {
                        // 顺便把贴图的X偏移量归零，如何定位一下要绘制的Buff图标行数
                        buffs[j] = (0, tooltips.Count, buffs[j].Item3, buffs[j].Item4, buffs[j].Item5, buffs[j].Item6);

                        // 同名Buff不再绘制
                        if (added.Contains(buffs[j].Item5))
                        {
                            buffs.Remove(buffs[j]);
                            continue;
                        }
                        else
                            added.Add(buffs[j].Item5);

                        // 添加Buff的名字和描述
                        tooltips.Add(new TooltipLine(this.Mod, "Hjson-ShowDetailBuffName", $"      [c/{buffs[j].Item3}:{buffs[j].Item5}]"));
                        tooltips.Add(new TooltipLine(this.Mod, "Hjson-ShowDetailBuffDescription", buffs[j].Item6));

                    }
                }
            }
        }
    }

    // 要绘制的Buff列表
    public List<(float, int, string, Texture2D, string, string)> buffs = new();
    // 给按Ctrl时候显示的信息筛重用的
    public List<string> added = new();

    public override void PostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines)
    {
        if (HjsonConfig.Instance.ImageTooltipBuff)
        {
            // 遍历当前物品描述列表
            for (int i = 0; i < lines.Count; i++)
            {
                // 查一下都要绘制哪些
                foreach (var buff in buffs)
                {
                    // 绘制匹配当前行数的Buff
                    if (buff.Item2 == i)
                        Main.spriteBatch.Draw(buff.Item4, new Vector2(lines[i].X + buff.Item1 + 5.5f, lines[i].Y - 7f), Color.White);
                    // 如果下一个Buff对应的行数大于当前行数就推出遍历，节省性能喵
                    if (buff.Item2 > i)
                        break;
                }
            }
            // 清空一下之前筛重用的List
            added.Clear();
        }
    }
}
