namespace VeluriyamLibHjson.Image;

internal class ImageTheTooltip : GlobalItem
{
    private static Regex MatchingBuff = new Regex(@"\[vbuff\/([^\]]+)\]");
    private static Regex MatchingDebuff = new Regex(@"\[vdebuff\/([^\]]+)\]");
    private static Regex MatchingSpecificBuff = new Regex(@"([^\/]+)\/([^\/]+)");

    public override bool InstancePerEntity => true;


    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        buffs.Clear();

        for (int i = 0; i < tooltips.Count; i++)
        {
            Texture2D texture = default;
            string name = default;
            string description = default;
            float length = default;

            void Match(Regex regex, string color)
            {
                // 修改Buff文本和add实例喵
                while (regex.Match(tooltips[i].Text).Success)
                {
                    tooltips[i].Text = regex.Replace(tooltips[i].Text, match =>
                    {
                        return MatchingSpecificBuff.Replace(match.Groups[1].Value, key =>
                        {
                            length = ChatManager.GetStringSize(FontAssets.MouseText.Value, tooltips[i].Text.Substring(0, match.Index), Vector2.One).X;
                            if (BuffID.Search.TryGetId(key.Groups[2].Value, out int buffType))
                            {
                                if (key.Groups[1].Value == "Terraria")
                                {
                                    texture = TextureAssets.Buff[buffType].Value;
                                    name = Lang.GetBuffName(buffType);
                                    description = Lang.GetBuffDescription(buffType);

                                    buffs.Add((length, i, color, texture, name, description));
                                }
                            }
                            else
                            {
                                if (ModLoader.TryGetMod(key.Groups[1].Value, out Mod source))
                                {
                                    if (source.TryFind<ModBuff>(key.Groups[2].Value, out ModBuff modbuff))
                                    {
                                        int x = 1;

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

            Match(MatchingBuff, "90EE90");
            Match(MatchingDebuff, "EE9090");
        }

        if (Main.keyState.IsKeyDown(Keys.LeftControl) && buffs.Count != 0)
        {
            if (tooltips.Count >= 2)
            {
                tooltips.RemoveRange(1, tooltips.Count - 1);

                for (int j = 0; j < buffs.Count; j++)
                {
                    buffs[j] = (0, tooltips.Count, buffs[j].Item3, buffs[j].Item4, buffs[j].Item5, buffs[j].Item6);

                    if (added.Contains(buffs[j].Item5))
                    {
                        buffs.Remove(buffs[j]);
                        continue;
                    }
                    else
                        added.Add(buffs[j].Item5);

                    tooltips.Add(new TooltipLine(this.Mod, "Hjson-ShowDetailBuffName", $"      [c/{buffs[j].Item3}:{buffs[j].Item5}]"));
                    tooltips.Add(new TooltipLine(this.Mod, "Hjson-ShowDetailBuffDescription", buffs[j].Item6));

                }
            }
        }
    }

    public List<(float, int, string, Texture2D, string, string)> buffs = new();
    public List<string> added = new();

    public override void PostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            foreach (var buff in buffs)
            {
                if (buff.Item2 == i)
                    Main.spriteBatch.Draw(buff.Item4, new Vector2(lines[i].X + buff.Item1 + 5.5f, lines[i].Y - 7f), Color.White);
                if (buff.Item2 > i)
                    break;
            }
        }
        added.Clear();
    }
}
