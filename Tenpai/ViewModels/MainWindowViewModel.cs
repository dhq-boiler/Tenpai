using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku;
using Tenpai.Models.Yaku.Meld;
using Tenpai.Models.Yaku.Meld.Detector;
using Tenpai.Views;
using Unity;

namespace Tenpai.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        [Dependency]
        public IDialogService dialogService { get; set; }

        [Dependency]
        public MainWindow MainWindow { get; set; }

        public ReactivePropertySlim<Tile> Tile0 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile1 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile2 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile3 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile4 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile5 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile6 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile7 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile8 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile9 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile10 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile11 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile12 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> Tile13 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>(Visibility.Collapsed, null));
        public ReactivePropertySlim<Tile> Tile14 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>(Visibility.Collapsed, null));
        public ReactivePropertySlim<Tile> Tile15 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>(Visibility.Collapsed, null));
        public ReactivePropertySlim<Tile> Tile16 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>(Visibility.Collapsed, null));
        public ReactivePropertySlim<Tile> AgariTile { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>(Visibility.Visible, null));
        public ReactivePropertySlim<Tile> DoraDisplayTile0 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> DoraDisplayTile1 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> DoraDisplayTile2 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> DoraDisplayTile3 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> DoraDisplayTile4 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> UraDoraDisplayTile0 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> UraDoraDisplayTile1 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> UraDoraDisplayTile2 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> UraDoraDisplayTile3 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<Tile> UraDoraDisplayTile4 { get; set; } = new ReactivePropertySlim<Tile>(Tile.CreateInstance<Dummy>());
        public ReactivePropertySlim<bool> IsArrangingTiles { get; } = new ReactivePropertySlim<bool>(true);
        public ReactiveCollection<MenuItem> ContextMenuItems { get; } = new ReactiveCollection<MenuItem>();
        public ReactiveCollection<MenuItem> SarashiHaiTripleContextMenuItems { get; } = new ReactiveCollection<MenuItem>();
        public ReactiveCommand<TilePlaceholder> SelectCommand { get; } = new ReactiveCommand<TilePlaceholder>();
        public ReactiveCommand<TilePlaceholder> SelectAgariTileCommand { get; } = new ReactiveCommand<TilePlaceholder>();
        public ReactiveCommand<string> ContextMenuOpeningCommand { get; } = new ReactiveCommand<string>();
        public ReactiveCommand<Tile> SarashiHaiTripleContextMenuOpeningCommand { get; } = new ReactiveCommand<Tile>();
        public ReactiveCommand<Call> PonCommand { get; } = new ReactiveCommand<Call>();
        public ReactiveCommand<Call> ChiCommand { get; } = new ReactiveCommand<Call>();
        public ReactiveCollection<Meld> SarashiHai { get; } = new ReactiveCollection<Meld>();
        public ReactiveCommand<Call> AnkanCommand { get; } = new ReactiveCommand<Call>();
        public ReactiveCommand<Call> DaiminkanCommand { get; } = new ReactiveCommand<Call>();
        public ReactiveCommand<Call> ShouminkanCommand { get; } = new ReactiveCommand<Call>();
        public ReactiveCollection<ReadyHand> ReadyHands { get; } = new ReactiveCollection<ReadyHand>();
        public ReactiveCollection<Yaku> Yakus { get;} = new ReactiveCollection<Yaku>();
        public ReactivePropertySlim<int> tileCount { get; } = new ReactivePropertySlim<int>(13);
        public ReactivePropertySlim<AgariType> AgariType { get; } = new ReactivePropertySlim<AgariType>();
        public ReadOnlyReactivePropertySlim<int> AgariTypeAsInt { get; }
        public ReactiveCommand ClosingCommand { get; } = new ReactiveCommand();
        public ReactivePropertySlim<WindOfTheRound> WindOfTheRound { get; } = new ReactivePropertySlim<WindOfTheRound>(ViewModels.WindOfTheRound.East);
        public ReactivePropertySlim<OnesOwnWind> OnesOwnWind { get; } = new ReactivePropertySlim<OnesOwnWind>(ViewModels.OnesOwnWind.East);

        private int sarashiCount = 0;

        public Tile[] Tiles { get { return new[] { Tile0.Value, Tile1.Value, Tile2.Value, Tile3.Value, Tile4.Value, Tile5.Value, Tile6.Value, Tile7.Value, Tile8.Value, Tile9.Value, Tile10.Value, Tile11.Value, Tile12.Value, Tile13.Value, Tile14.Value, Tile15.Value, Tile16.Value, AgariTile.Value }; } }
        public Tile[] TilesWithoutAgariTile { get { return new[] { Tile0.Value, Tile1.Value, Tile2.Value, Tile3.Value, Tile4.Value, Tile5.Value, Tile6.Value, Tile7.Value, Tile8.Value, Tile9.Value, Tile10.Value, Tile11.Value, Tile12.Value, Tile13.Value, Tile14.Value, Tile15.Value, Tile16.Value }; } }

        private bool sortflag = false;

        public MainWindowViewModel()
        {
            ContextMenuOpeningCommand.Subscribe(args =>
            {
                ContextMenuItems.Clear();

                if (Tiles.Count(x => x.EqualsRedSuitedTileIncluding(Tiles[int.Parse(args)])) >= 2 && Tiles.Count(x => x.EqualsRedSuitedTileIncluding(Tiles[int.Parse(args)])) <= 3)
                {
                    var pon = new MenuItem() { Header = "ポン" };
                    var ponIncompletedMelds = IncompletedMeldDetector.FindIncompletedTriple(Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(Tiles[int.Parse(args)]));
                    var ponCompletedMelds = ConvertToCompletedTriple(ponIncompletedMelds).Where(x => x.Tiles.Contains(Tiles[int.Parse(args)]));
                    foreach (var completedMeld in ponCompletedMelds)
                    {
                        var ponCandidate = new PonCandidate()
                        {
                            DataContext = completedMeld,
                        };
                        var ponCandidateMenuItem = new MenuItem()
                        {
                            Header = ponCandidate,
                            Command = PonCommand,
                            CommandParameter = new Call(Tiles[int.Parse(args)], completedMeld.CallFrom.Value, completedMeld),
                        };
                        pon.Items.Add(ponCandidateMenuItem);
                    }
                    try
                    {
                        ContextMenuItems.Add(pon);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                }

                var incompletedMelds = IncompletedMeldDetector.FindIncompletedRuns(Tiles.Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(Tiles[int.Parse(args)]));
                var completedMelds = ConvertToCompletedRuns(incompletedMelds).Where(x => x.Tiles.Contains(Tiles[int.Parse(args)]));
                if (completedMelds.Any())
                {
                    var chi = new MenuItem() { Header = "チー" };
                    foreach (var completedMeld in completedMelds)
                    {
                        var chiCandidate = new ChiCandidate()
                        {
                            DataContext = completedMeld,
                        };
                        var chiCandidateMenuItem = new MenuItem()
                        {
                            Header = chiCandidate,
                            Command = ChiCommand,
                            CommandParameter = new Call(Tiles[int.Parse(args)], completedMeld.CallFrom.Value, completedMeld),
                        };
                        chi.Items.Add(chiCandidateMenuItem);
                    }
                    try
                    {
                        ContextMenuItems.Add(chi);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                }

                var removeTiles = new List<Tile>();
                if (AgariType.Value == ViewModels.AgariType.Ron)
                {
                    removeTiles.Add(AgariTile.Value);
                }
                var quads = MeldDetector.FindQuads(Tiles.Except(removeTiles).Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.Tiles.Contains(Tiles[int.Parse(args)]));
                var incompletedQuads = IncompletedMeldDetector.FindIncompletedQuad(Tiles.Except(removeTiles).Where(x => x.Visibility.Value == Visibility.Visible && !(x is Dummy)).ToArray()).Where(x => x.AllTiles.Contains(Tiles[int.Parse(args)]));
                var daiminkanCompletedQuads = ConvertToCompletedQuads(incompletedQuads).Where(x => x.Tiles.Contains(Tiles[int.Parse(args)]));
                if (quads.Any() || daiminkanCompletedQuads.Any())
                {
                    var kan = new MenuItem() { Header = "カン" };
                    if (quads.Any())
                    {
                        foreach (var quad in quads)
                        {
                            (quad as Quad).Type = Models.Yaku.Meld.KongType.ConcealedKong;
                            var ankanCandidate = new AnkanCandidate()
                            {
                                DataContext = quad,
                            };
                            var ankanCandidateMenuItem = new MenuItem()
                            {
                                Header = ankanCandidate,
                                Command = AnkanCommand,
                                CommandParameter = new Call(Tiles[int.Parse(args)], quad)
                            };
                            kan.Items.Add(ankanCandidateMenuItem);
                        }
                    }

                    if (!quads.Any() && daiminkanCompletedQuads.Any())
                    {
                        foreach (var daiminkanQuad in daiminkanCompletedQuads)
                        {
                            (daiminkanQuad as Quad).Type = Models.Yaku.Meld.KongType.LargeMeldedKong;
                            var daiminkanCandidate = new DaiminkanCandidate()
                            {
                                DataContext = daiminkanQuad,
                            };
                            var daiminkanCandidateMenuItem = new MenuItem()
                            {
                                Header = daiminkanCandidate,
                                Command = DaiminkanCommand,
                                CommandParameter = new Call(Tiles[int.Parse(args)], daiminkanQuad.CallFrom.Value, daiminkanQuad)
                            };
                            kan.Items.Add(daiminkanCandidateMenuItem);
                        }
                    }

                    try
                    {
                        ContextMenuItems.Add(kan);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                }
            })
            .AddTo(_disposables);
            SarashiHaiTripleContextMenuOpeningCommand.Subscribe(args =>
            {
                SarashiHaiTripleContextMenuItems.Clear();
                var targetTile = args;
                var containsOneTile = Tiles.Where(y => y.Visibility.Value == Visibility.Visible).Count(y => y.EqualsRedSuitedTileIncluding(targetTile)) == 1;
                var targetCalledTriples = SarashiHai.Where(x => x.Tiles.Contains(targetTile));
                if (targetCalledTriples.Any() && containsOneTile)
                {
                    var targetCalledTriple = targetCalledTriples.First();
                    var kan = new MenuItem() { Header = "カン" };
                    Quad quad = null;
                    var tiles = targetCalledTriple.Tiles;
                    switch (targetCalledTriple.CallFrom)
                    {
                        case EOpponent.Kamicha:
                            targetTile = targetTile.Clone() as Tile;
                            tiles[0].Rotate = new System.Windows.Media.RotateTransform(90);
                            targetTile.Rotate = new System.Windows.Media.RotateTransform(90);
                            tiles[1].Rotate = null;
                            tiles[2].Rotate = null;
                            quad = new Quad(tiles[0], targetTile, tiles[1], tiles[2]);
                            break;
                        case EOpponent.Toimen:
                            targetTile = targetTile.Clone() as Tile;
                            tiles[0].Rotate = null;
                            tiles[1].Rotate = new System.Windows.Media.RotateTransform(90);
                            targetTile.Rotate = new System.Windows.Media.RotateTransform(90);
                            tiles[2].Rotate = null;
                            quad = new Quad(tiles[0], tiles[1], targetTile, tiles[2]);
                            break;
                        case EOpponent.Shimocha:
                            targetTile = targetTile.Clone() as Tile;
                            tiles[0].Rotate = null;
                            tiles[1].Rotate = null;
                            tiles[2].Rotate = new System.Windows.Media.RotateTransform(90);
                            targetTile.Rotate = new System.Windows.Media.RotateTransform(90);
                            quad = new Quad(tiles[0], tiles[1], tiles[2], targetTile);
                            break;
                    }

                    quad.Type = Models.Yaku.Meld.KongType.SmallMeldedKong;
                    var shouminkanCandidate = new ShouminkanCandidate()
                    {
                        DataContext = quad,
                    };
                    var shouminkanCandidateMenuItem = new MenuItem()
                    {
                        Header = shouminkanCandidate,
                        Command = ShouminkanCommand,
                        CommandParameter = new Call(targetTile, quad)
                    };
                    kan.Items.Add(shouminkanCandidateMenuItem);

                    try
                    {
                        SarashiHaiTripleContextMenuItems.Add(kan);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                }
            })
            .AddTo(_disposables);
            PonCommand.Where(x => Tiles.Count(y => y.EqualsRedSuitedTileIncluding(x.Target)) >= 2 && Tiles.Count(y => y.EqualsRedSuitedTileIncluding(x.Target)) <= 3)
                      .Select(x => x)
                      .Subscribe(args =>
            {
                var exist = args.Target;
                var target = args.Target;
                var targetTiles = Tiles.Where(x => x != null && x.Code == target.Code);
                if (targetTiles.First() is IRedSuitedTile)
                {
                    if (!ContainsRedTile(targetTiles))
                    {
                        IDialogResult dialogResult = null;
                        dialogService.ShowDialog(nameof(SelectRedTileOrNot), new DialogParameters() { { "Tile", Tile.CreateInstance(targetTiles.First()) }, { "RedTile", Tile.CreateRedInstance(targetTiles.First().Code, targetTiles.First().Visibility.Value, null) } }, (result) =>
                         {
                             dialogResult = result;
                         });
                        if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                        {
                            target = dialogResult.Parameters.GetValue<Tile>("Result");
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        target = Tile.CreateInstance(args.Target.Code, args.Target.Visibility.Value, null);
                    }
                }

                sarashiCount += 3;

                var rotate = target.Clone() as Tile;
                rotate.CallFrom = args.CallFrom;
                if (rotate.CallFrom == EOpponent.Kamicha)
                    rotate.Order = 0;
                else if (rotate.CallFrom == EOpponent.Toimen)
                    rotate.Order = 1;
                else if (rotate.CallFrom == EOpponent.Shimocha)
                    rotate.Order = 2;
                rotate.Rotate = new System.Windows.Media.RotateTransform(90);

                UpdateTile(new Dummy(), rotate, 1);
                UpdateTileVisibilityToCollapsed(rotate.Code, 3);
                var updateOrderList = Tiles.Where(x => x.Code.Equals(rotate.Code) && x.Order >= rotate.Order && x.CallFrom != rotate.CallFrom).ToList();
                updateOrderList.ForEach(x => x.Order++);

                SortIf();
                
                switch (args.CallFrom)
                {
                    case EOpponent.Kamicha:
                        SarashiHai.Add(new Triple(rotate, targetTiles.ElementAt(0), targetTiles.ElementAt(1)));
                        break;
                    case EOpponent.Toimen:
                        SarashiHai.Add(new Triple(targetTiles.ElementAt(0), rotate, targetTiles.ElementAt(1)));
                        break;
                    case EOpponent.Shimocha:
                        SarashiHai.Add(new Triple(targetTiles.ElementAt(0), targetTiles.ElementAt(1), rotate));
                        break;
                }
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            ChiCommand.Subscribe(args =>
            {
                sarashiCount += 3;
                var rotate = args.Meld.Tiles.First(x => x.Rotate is not null) as Tile;
                if (Tiles.Where(x => x.Code == rotate.Code).Any())
                {
                    rotate.Order = Tiles.Where(x => x.Code == rotate.Code).Max(x => x.Order) + 1;
                }
                rotate.CallFrom = args.CallFrom;
                rotate.Rotate = new System.Windows.Media.RotateTransform(90);
                UpdateTile(new Dummy(), rotate, 1);
                foreach (var tile in args.Meld.Tiles.Where(x => x.CallFrom == EOpponent.Default))
                {
                    UpdateTileVisibility(tile, 1);
                }
                UpdateTileVisibility(rotate, 1);
                SarashiHai.Add(args.Meld);
                ConstructHand();
            })
            .AddTo(_disposables);
            AnkanCommand.Subscribe(args =>
            {
                sarashiCount += 4;
                tileCount.Value++;
                if (args.Target.EqualsRedSuitedTileIncluding(AgariTile.Value))
                {
                    UpdateTile(new Dummy(), AgariTile.Value, 1);
                    AgariTile.Value = Tile.CreateInstance<Dummy>(Visibility.Visible, null);
                }
                for (int i = 0; i < 4; i++)
                {
                    UpdateTileVisibility(args.Meld.Tiles[i], 1);
                }
                SarashiHai.Add(args.Meld);
                ConstructHand();
            })
            .AddTo(_disposables);
            DaiminkanCommand.Where(x => Tiles.Count(y => y.EqualsRedSuitedTileIncluding(x.Target)) == 3)
                      .Select(x => x)
                      .Subscribe(args =>
                      {
                          var exist = args.Target;
                          var target = args.Target;
                          var targetTiles = Tiles.Where(x => x != null && x.Code == target.Code);
                          
                          sarashiCount += 4;
                          tileCount.Value++;

                          var rotate = target.Clone() as Tile;
                          if (args.CallFrom == EOpponent.Kamicha)
                              args.Target.Order = 0;
                          else if (args.CallFrom == EOpponent.Toimen)
                              args.Target.Order = 1;
                          else if (args.CallFrom == EOpponent.Shimocha)
                              args.Target.Order = 3;
                          rotate.CallFrom = args.CallFrom;
                          rotate.Rotate = new System.Windows.Media.RotateTransform(90);
                          
                          if (target.EqualsRedSuitedTileIncluding(AgariTile.Value))
                          {
                              AgariTile.Value = Tile.CreateInstance<Dummy>(Visibility.Visible, null);
                          }

                          UpdateTile(new Dummy(), rotate, 1);
                          UpdateTileVisibilityToCollapsed(rotate.Code, 4);

                          var updateOrderList = Tiles.Where(x => x.Code.Equals(rotate.Code) && x.Order >= rotate.Order && x.CallFrom != rotate.CallFrom).ToList();
                          updateOrderList.ForEach(x => x.Order++);

                          SortIf();

                          Quad quad = null;
                          switch (args.CallFrom)
                          {
                              case EOpponent.Kamicha:
                                  quad = new Quad(rotate, targetTiles.ElementAt(0), targetTiles.ElementAt(1), targetTiles.ElementAt(2));
                                  quad.Type = Models.Yaku.Meld.KongType.LargeMeldedKong;
                                  SarashiHai.Add(quad);
                                  break;
                              case EOpponent.Toimen:
                                  quad = new Quad(targetTiles.ElementAt(0), rotate, targetTiles.ElementAt(1), targetTiles.ElementAt(2));
                                  quad.Type = Models.Yaku.Meld.KongType.LargeMeldedKong;
                                  SarashiHai.Add(quad);
                                  break;
                              case EOpponent.Shimocha:
                                  quad = new Quad(targetTiles.ElementAt(0), targetTiles.ElementAt(1), targetTiles.ElementAt(2), rotate);
                                  quad.Type = Models.Yaku.Meld.KongType.LargeMeldedKong;
                                  SarashiHai.Add(quad);
                                  break;
                          }
                          SortIf();
                          ConstructHand();
                      })
            .AddTo(_disposables);
            ShouminkanCommand.Where(x => {
                var first = Tiles.Where(y => y.Visibility.Value == Visibility.Visible).Count(y => y.EqualsRedSuitedTileIncluding(x.Target)) == 1;
                var second = SarashiHai.Where(y => y.Tiles[0].EqualsRedSuitedTileIncluding(x.Target) && y is Triple).Any();
                Console.WriteLine($"ShouminkanCommand_CanExecute first:{first}, second:{second}");
                return first && second;
                                   })
                             .Subscribe(args =>
            {
                sarashiCount++;
                tileCount.Value++;
                args.Target.CallFrom = args.CallFrom;
                args.Target.Rotate = new System.Windows.Media.RotateTransform(90);
                
                var targetCalledTriple = SarashiHai.First(x => x is Triple t && t.Tiles.All(x => x.EqualsRedSuitedTileIncluding(args.Target)));

                if (targetCalledTriple.CallFrom == EOpponent.Kamicha)
                    args.Target.Order = 1;
                else if (targetCalledTriple.CallFrom == EOpponent.Toimen)
                    args.Target.Order = 2;
                else if (targetCalledTriple.CallFrom == EOpponent.Shimocha)
                    args.Target.Order = 3;
                var updateOrderList = Tiles.Where(x => x.Code.Equals(args.Target.Code) && x.Order >= args.Target.Order && x.CallFrom != args.Target.CallFrom).ToList();
                updateOrderList.ForEach(x => x.Order++);

                SortIf();

                targetCalledTriple.Tiles.Add(args.Target);

                UpdateTileVisibilityToCollapsed(args.Target.Code, 1);
                
                var index = SarashiHai.IndexOf(targetCalledTriple);
                (args.Meld as Quad).Type = Models.Yaku.Meld.KongType.SmallMeldedKong;
                SarashiHai[index] = args.Meld;
                ConstructHand();
            })
            .AddTo(_disposables);
            SelectCommand.Subscribe(tp =>
            {
                IDialogResult dialogResult = null;
                dialogService.ShowDialog(nameof(TileLineup), (result) =>
                {
                    dialogResult = result;
                });
                if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                {
                    var newTile = dialogResult.Parameters.GetValue<Tile>("TileType");
                    newTile.Order = Tiles.Where(x => x.Code == newTile.Code).Count();
                    tp.TileType = newTile;
                }
            })
            .AddTo(_disposables);
            SelectAgariTileCommand.Subscribe(tp =>
            {
                var targetsWithoutDummy = Tiles.Where(x => !(x is Dummy));
                var targets = targetsWithoutDummy;
                var count = targets.Count();
                if (count >= tileCount.Value)
                {
                    var position = GetMousePosition();
                    IDialogResult dialogResult = null;
                    ConstructReadyHands();
                    if (Yakus.Single(x => x is AddAQuad).IsEnable.Value && ReadyHands.Count() > 0)
                    {
                        dialogService.ShowDialog(nameof(Ron), new DialogParameters() { { "Left", position.X }, { "Top", position.Y } }, (result) =>
                        {
                            dialogResult = result;
                        });
                    }
                    else
                    {
                        dialogService.ShowDialog(nameof(TumoOrRon), new DialogParameters() { { "Left", position.X }, { "Top", position.Y } }, (result) =>
                        {
                            dialogResult = result;
                        });
                    }
                    if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
                    {
                        IDialogResult dialogResult2 = null;
                        dialogService.ShowDialog(nameof(TileLineup), (result) =>
                        {
                            dialogResult2 = result;
                        });
                        if (dialogResult2 != null && dialogResult2.Result == ButtonResult.OK)
                        {
                            var newTile = dialogResult2.Parameters.GetValue<Tile>("TileType");
                            newTile.Order = Tiles.Where(x => x.Code == newTile.Code).Count();
                            AgariType.Value = dialogResult.Parameters.GetValue<AgariType>("AgariType");
                            tp.TileType = newTile;
                        }
                    }
                }
            })
            .AddTo(_disposables);
            Tile0.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile1.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile2.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile3.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile4.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile5.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile6.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile7.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile8.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile9.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile10.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile11.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile12.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile13.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile14.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile15.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            Tile16.Subscribe(_ =>
            {
                SortIf();
                ConstructHand();
            })
            .AddTo(_disposables);
            DoraDisplayTile0.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            DoraDisplayTile1.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            DoraDisplayTile2.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            DoraDisplayTile3.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            DoraDisplayTile4.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            UraDoraDisplayTile0.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            UraDoraDisplayTile1.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            UraDoraDisplayTile2.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            UraDoraDisplayTile3.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            UraDoraDisplayTile4.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            AgariTile.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            IsArrangingTiles.Subscribe(flag =>
            {
                SortIf();
            })
            .AddTo(_disposables);
            tileCount.Subscribe(count =>
            {
                switch (count)
                {
                    case 14:
                        Tile13.Value.Visibility.Value = Visibility.Visible;
                        break;
                    case 15:
                        Tile14.Value.Visibility.Value = Visibility.Visible;
                        break;
                    case 16:
                        Tile15.Value.Visibility.Value = Visibility.Visible;
                        break;
                    case 17:
                        Tile16.Value.Visibility.Value = Visibility.Visible;
                        break;
                }
            })
            .AddTo(_disposables);
            ClosingCommand.Subscribe(_ =>
            {
                App.Current.Shutdown();
            })
            .AddTo(_disposables);
            AgariTypeAsInt = AgariType.Select(x => (int)x)
                                      .ToReadOnlyReactivePropertySlim();
            AgariType.Subscribe(x =>
            {
                if (x is ViewModels.AgariType.Tsumo)
                {
                    SwitchIsEnable<FinalTileWin_Ron>(false);
                }
                else if (x is ViewModels.AgariType.Ron)
                {
                    SwitchIsEnable<FinalTileWin_Tumo>(false);
                }
            })
            .AddTo(_disposables);
            WindOfTheRound.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            OnesOwnWind.Subscribe(_ =>
            {
                ConstructHand();
            })
            .AddTo(_disposables);
            Yakus.Add(new Reach()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<FirstTurnWin>(false);
                    SwitchIsEnable<DoubleReach>(false);
                    ConstructHand();
                }),
            });
            Yakus.Add(new FirstTurnWin()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    SwitchIsEnable<Reach>(true);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new DoubleReach()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    SwitchIsEnable<Reach>(true);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new KingsTileDraw()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new FinalTileWin_Tumo()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    SwitchIsEnable<FinalTileWin_Ron>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new FinalTileWin_Ron()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    SwitchIsEnable<FinalTileWin_Tumo>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new HeavenlyWin()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<Reach>(false);
                    SwitchIsEnable<FirstTurnWin>(false);
                    SwitchIsEnable<DoubleReach>(false);
                    SwitchIsEnable<KingsTileDraw>(false);
                    SwitchIsEnable<FinalTileWin_Tumo>(false);
                    SwitchIsEnable<FinalTileWin_Ron>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                    SwitchIsEnable<AddAQuad>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new EarthlyWin()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<Reach>(false);
                    SwitchIsEnable<FirstTurnWin>(false);
                    SwitchIsEnable<DoubleReach>(false);
                    SwitchIsEnable<KingsTileDraw>(false);
                    SwitchIsEnable<FinalTileWin_Tumo>(false);
                    SwitchIsEnable<FinalTileWin_Ron>(false);
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<AddAQuad>(false);
                    ConstructHand();
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
            Yakus.Add(new AddAQuad()
            {
                CheckedCommand = new DelegateCommand(() =>
                {
                    SwitchIsEnable<HeavenlyWin>(false);
                    SwitchIsEnable<EarthlyWin>(false);
                }),
                UncheckedCommand = new DelegateCommand(() =>
                {
                    ConstructHand();
                }),
            });
        }

        private void ConstructHand()
        {
            if (tileCount.Value + 1 == Tiles.Where(x => x is not Dummy).Count())
            {
                ConstructCompleteHands();
            }
            else if (tileCount.Value == Tiles.Where(x => x is not Dummy).Count())
            {
                ConstructReadyHands();
            }
            else
            {
                ReadyHands.Clear();
            }
        }

        private void ConstructCompleteHands()
        {
            SortIf();
            ReadyHands.Clear();
            var tiles = Tiles.Where(x => !(x is Dummy)).ToList();
            var readyHands = MeldDetector.FindCompletedHands(tiles.ToArray(), SarashiHai.ToArray(), tileCount.Value + 1, AgariType.Value, AgariTile.Value, WindOfTheRound.Value, OnesOwnWind.Value, new DoraDisplayTileCollection(new Tile[] { DoraDisplayTile0.Value, DoraDisplayTile1.Value, DoraDisplayTile2.Value, DoraDisplayTile3.Value, DoraDisplayTile4.Value }), new DoraDisplayTileCollection(new Tile[] { UraDoraDisplayTile0.Value, UraDoraDisplayTile1.Value, UraDoraDisplayTile2.Value, UraDoraDisplayTile3.Value, UraDoraDisplayTile4.Value })).ToList();
            readyHands.ToList().ForEach(x =>
            {
                x.Yakus.AddRange(this.Yakus.Where(y => y.IsEnable.Value));
                MeldDetector.CalcScore(ref readyHands, OnesOwnWind.Value);
            });
            RemoveUnder12HanYakuFromYakuList(readyHands);
            ReadyHands.AddRange(readyHands.OrderByDescending(x => x.Score).ThenByDescending(x => x.SumHanCount).ThenByDescending(x => x.HuSum));
        }

        private void ConstructReadyHands()
        {
            SortIf();
            ReadyHands.Clear();
            var readyHands = MeldDetector.FindReadyHands(TilesWithoutAgariTile.Where(x => !(x is Dummy)).ToArray(), SarashiHai.ToArray(), tileCount.Value, AgariType.Value, WindOfTheRound.Value, OnesOwnWind.Value, new DoraDisplayTileCollection(new Tile[] { DoraDisplayTile0.Value, DoraDisplayTile1.Value, DoraDisplayTile2.Value, DoraDisplayTile3.Value, DoraDisplayTile4.Value }), new DoraDisplayTileCollection(new Tile[] { UraDoraDisplayTile0.Value, UraDoraDisplayTile1.Value, UraDoraDisplayTile2.Value, UraDoraDisplayTile3.Value, UraDoraDisplayTile4.Value })).OrderBy(x => x.WaitingTiles[0]).ToList();
            readyHands.ToList().ForEach(x =>
            {
                x.Yakus.AddRange(this.Yakus.Where(y => y.IsEnable.Value));
                MeldDetector.CalcScore(ref readyHands, OnesOwnWind.Value);
            });
            RemoveUnder12HanYakuFromYakuList(readyHands);
            ReadyHands.AddRange(readyHands.OrderByDescending(x => x.Score).ThenByDescending(x => x.SumHanCount).ThenByDescending(x => x.HuSum));
        }

        private void RemoveUnder12HanYakuFromYakuList<T>(List<T> readyHands) where T : ReadyHand
        {
            readyHands.ToList().ForEach(x =>
            {
                if (x.Yakus.Count(y => y.HanCount(SarashiHai.Where(x => (x is Quad q && q.Type != KongType.ConcealedKong) || (x is not Quad)).Count() > 0) >= 13) > 0) //役満以上の役が1つ以上成立している
                {
                    var under12hanYakus = x.Yakus.Where(x => x.HanCount(SarashiHai.Where(x => (x is Quad q && q.Type != KongType.ConcealedKong) || (x is not Quad)).Count() > 0) <= 12).ToList();
                    foreach (var under12hanYaku in under12hanYakus)
                    {
                        x.Yakus.Remove(under12hanYaku);
                    }
                }
                if (AgariType.Value != ViewModels.AgariType.Ron)
                {
                    var addAQuad = x.Yakus.Where(x => x is AddAQuad).SingleOrDefault();
                    if (addAQuad != null)
                    {
                        x.Yakus.Remove(addAQuad);
                    }
                }
            });
        }

        public static Point GetMousePosition()
        {
            Utils.NativeMethods.Win32Point w32Mouse = new Utils.NativeMethods.Win32Point();
            Utils.NativeMethods.GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private void SwitchIsEnable<T>(bool isEnable)
        {
            Yakus.First(x => x is T).IsEnable.Value = isEnable;
        }

        public Meld[] ConvertToCompletedQuads(IEnumerable<Meld> incompletedQuads)
        {
            var callFroms = new[] { EOpponent.Kamicha, EOpponent.Toimen, EOpponent.Shimocha };

            var melds = new List<Meld>();
            foreach (var incompletedQuad in incompletedQuads)
            {
                if (incompletedQuad is Triple t)
                {
                    foreach (var callFrom in callFroms)
                    {
                        var _wait = t.Tiles[0].Clone() as Tile;
                        _wait.Rotate = new System.Windows.Media.RotateTransform(90);
                        _wait.CallFrom = callFrom;
                        switch (_wait.CallFrom)
                        {
                            case EOpponent.Kamicha:
                                melds.Add(new Quad(_wait, t.Tiles[0], t.Tiles[1], t.Tiles[2]));
                                break;
                            case EOpponent.Toimen:
                                melds.Add(new Quad(t.Tiles[0], _wait, t.Tiles[1], t.Tiles[2]));
                                break;
                            case EOpponent.Shimocha:
                                melds.Add(new Quad(t.Tiles[0], t.Tiles[1], t.Tiles[2], _wait));
                                break;
                        }
                    }
                }
            }
            return melds.ToArray();
        }

        private Meld[] ConvertToCompletedTriple(IEnumerable<IncompletedMeld> ponIncompletedMelds)
        {
            var callFroms = new[] { EOpponent.Kamicha, EOpponent.Toimen, EOpponent.Shimocha };

            var melds = new List<Meld>();
            foreach (var incompletedMeld in ponIncompletedMelds)
            {
                if (incompletedMeld is Models.Yaku.Meld.Double d)
                {
                    d.ComputeWaitTiles();
                    foreach (var wait in d.WaitTiles)
                    {
                        foreach (var callFrom in callFroms)
                        {
                            var _wait = wait.Clone() as Tile;
                            _wait.Rotate = new System.Windows.Media.RotateTransform(90);
                            _wait.CallFrom = callFrom;
                            switch (_wait.CallFrom)
                            {
                                case EOpponent.Kamicha:
                                    melds.Add(new Triple(_wait, d.Tiles[0], d.Tiles[1]));
                                    break;
                                case EOpponent.Toimen:
                                    melds.Add(new Triple(d.Tiles[0], _wait, d.Tiles[1]));
                                    break;
                                case EOpponent.Shimocha:
                                    melds.Add(new Triple(d.Tiles[0], d.Tiles[1], _wait));
                                    break;
                            }
                        }
                    }
                }
            }
            return melds.ToArray();
        }

        private bool ContainsRedTile(IEnumerable<Tile> targetTiles)
        {
            foreach (var tile in targetTiles)
            {
                if (tile is IRedSuitedTile r && r.IsRedSuited == true)
                    return true;
            }
            return false;
        }

        private void UpdateTile(Tile replaced, Tile target, int count)
        {
            int processedCount = 0;
            for (int j = 0; j < Tiles.Count(); j++)
            {
                var tile = GetTile(j);
                if (tile is null || tile.Visibility.Value == Visibility.Collapsed)
                    continue;
                if (tile.Equals(replaced) && tile.Visibility.Value != Visibility.Collapsed && processedCount < count)
                {
                    SetTile(j, target);
                    processedCount++;
                }
                if (processedCount == count)
                {
                    return;
                }
            }
        }

        public Meld[] ConvertToCompletedRuns(IEnumerable<IncompletedMeld> incompletedMelds)
        {
            var callFroms = new[] { EOpponent.Kamicha, EOpponent.Toimen, EOpponent.Shimocha };

            var melds = new List<Meld>();
            foreach (var incompletedMeld in incompletedMelds)
            {
                if (incompletedMeld is OpenWait o)
                {
                    foreach (var wait in o.WaitTiles)
                    {
                        foreach (var callFrom in callFroms)
                        {
                            var _wait = wait.Clone() as Tile;
                            _wait.Rotate = new System.Windows.Media.RotateTransform(90);
                            _wait.CallFrom = callFrom;
                            switch (_wait.CallFrom)
                            {
                                case EOpponent.Kamicha:
                                    melds.Add(new Run(_wait, o.Tiles[0], o.Tiles[1]));
                                    break;
                                case EOpponent.Toimen:
                                    melds.Add(new Run(o.Tiles[0], _wait, o.Tiles[1]));
                                    break;
                                case EOpponent.Shimocha:
                                    melds.Add(new Run(o.Tiles[0], o.Tiles[1], _wait));
                                    break;
                            }
                        }
                    }
                }
                else if (incompletedMeld is ClosedWait c)
                {
                    foreach (var wait in c.WaitTiles)
                    {
                        foreach (var callFrom in callFroms)
                        {
                            var _wait = wait.Clone() as Tile;
                            _wait.Rotate = new System.Windows.Media.RotateTransform(90);
                            _wait.CallFrom = callFrom;
                            switch (_wait.CallFrom)
                            {
                                case EOpponent.Kamicha:
                                    melds.Add(new Run(_wait, c.Tiles[0], c.Tiles[1]));
                                    break;
                                case EOpponent.Toimen:
                                    melds.Add(new Run(c.Tiles[0], _wait, c.Tiles[1]));
                                    break;
                                case EOpponent.Shimocha:
                                    melds.Add(new Run(c.Tiles[0], c.Tiles[1], _wait));
                                    break;
                            }
                        }
                    }
                }
                else if (incompletedMeld is EdgeWait e)
                {
                    foreach (var wait in e.WaitTiles)
                    {
                        foreach (var callFrom in callFroms)
                        {
                            var _wait = wait.Clone() as Tile;
                            _wait.Rotate = new System.Windows.Media.RotateTransform(90);
                            _wait.CallFrom = callFrom;
                            switch (_wait.CallFrom)
                            {
                                case EOpponent.Kamicha:
                                    melds.Add(new Run(_wait, e.Tiles[0], e.Tiles[1]));
                                    break;
                                case EOpponent.Toimen:
                                    melds.Add(new Run(e.Tiles[0], _wait, e.Tiles[1]));
                                    break;
                                case EOpponent.Shimocha:
                                    melds.Add(new Run(e.Tiles[0], e.Tiles[1], _wait));
                                    break;
                            }
                        }
                    }
                }
            }
            return melds.ToArray();
        }

        private void UpdateTileVisibility(Tile args, int count)
        {
            int processedCount = 0;
            for (int j = 0; j < tileCount.Value; j++)
            {
                var tile = GetTile(j);
                if (tile is null)
                    continue;
                if (tile.Equals(args) && tile.Visibility.Value != Visibility.Collapsed)
                {
                    tile.Visibility.Value = Visibility.Collapsed;
                    processedCount++;
                }
                if (processedCount == count)
                    return;
            }
        }

        private void UpdateTileVisibilityToCollapsed(int code, int count)
        {
            int processedCount = 0;
            for (int j = 0; j < tileCount.Value; j++)
            {
                var tile = GetTile(j);
                if (tile is null)
                    continue;
                if (tile.Code == code && tile.Visibility.Value != Visibility.Collapsed)
                {
                    tile.Visibility.Value = Visibility.Collapsed;
                    processedCount++;
                }
                if (processedCount == count)
                    return;
            }
        }

        private void RemoveTiles(Tile args, int v)
        {
            var count = Tiles.Where(x => x != null && x.CompareTo(args) == 0).Count();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < Tiles.Count(); j++)
                {
                    var targetTile = Tiles[j];
                    if (targetTile == null)
                        continue;
                    if (args.CompareTo(targetTile) == 0)
                    {
                        SetTile(j, null);
                    }
                }
                SortIf();
            }
        }

        private void SortIf()
        {
            if (sortflag)
                return;
            sortflag = true;
            if (IsArrangingTiles.Value)
                ArrangeTiles();
            sortflag = false;
        }

        private void ArrangeTiles()
        {
            var tiles = new[] { Tile0.Value, Tile1.Value, Tile2.Value, Tile3.Value, Tile4.Value, Tile5.Value, Tile6.Value, Tile7.Value, Tile8.Value, Tile9.Value, Tile10.Value, Tile11.Value, Tile12.Value, Tile13.Value, Tile14.Value, Tile15.Value, Tile16.Value }.ToList();
            tiles.Sort();
            Tile0.Value = tiles[0];
            Tile1.Value = tiles[1];
            Tile2.Value = tiles[2];
            Tile3.Value = tiles[3];
            Tile4.Value = tiles[4];
            Tile5.Value = tiles[5];
            Tile6.Value = tiles[6];
            Tile7.Value = tiles[7];
            Tile8.Value = tiles[8];
            Tile9.Value = tiles[9];
            Tile10.Value = tiles[10];
            Tile11.Value = tiles[11];
            Tile12.Value = tiles[12];
            Tile13.Value = tiles[13];
            Tile14.Value = tiles[14];
            Tile15.Value = tiles[15];
            Tile16.Value = tiles[16];
            Console.WriteLine("Arranged");
            Trace.WriteLine("Arranged");
        }

        private Tile GetTile(int i)
        {
            switch (i)
            {
                case 0:
                    return Tile0.Value;
                case 1:
                    return Tile1.Value;
                case 2:
                    return Tile2.Value;
                case 3:
                    return Tile3.Value;
                case 4:
                    return Tile4.Value;
                case 5:
                    return Tile5.Value;
                case 6:
                    return Tile6.Value;
                case 7:
                    return Tile7.Value;
                case 8:
                    return Tile8.Value;
                case 9:
                    return Tile9.Value;
                case 10:
                    return Tile10.Value;
                case 11:
                    return Tile11.Value;
                case 12:
                    return Tile12.Value;
                case 13:
                    return Tile13.Value;
                case 14:
                    return Tile14.Value;
                case 15:
                    return Tile15.Value;
                case 16:
                    return Tile16.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ClearTiles()
        {
            for (int i = 0; i < Tiles.Count(); i++)
            {
                Tile0.Value = null;
            }
        }

        private void SetTile(int i, Tile tile)
        {
            switch (i)
            {
                case 0:
                    Console.WriteLine($"Tile{i}: {Tile0.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile0.Value.Description} = {tile.Description}");
                    Tile0.Value = tile.Clone() as Tile;
                    break;
                case 1:
                    Console.WriteLine($"Tile{i}: {Tile1.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile1.Value.Description} = {tile.Description}");
                    Tile1.Value = tile.Clone() as Tile;
                    break;
                case 2:
                    Console.WriteLine($"Tile{i}: {Tile2.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile2.Value.Description} = {tile.Description}");
                    Tile2.Value = tile.Clone() as Tile;
                    break;
                case 3:
                    Console.WriteLine($"Tile{i}: {Tile3.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile3.Value.Description} = {tile.Description}");
                    Tile3.Value = tile.Clone() as Tile;
                    break;
                case 4:
                    Console.WriteLine($"Tile{i}: {Tile4.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile4.Value.Description} = {tile.Description}");
                    Tile4.Value = tile.Clone() as Tile;
                    break;
                case 5:
                    Console.WriteLine($"Tile{i}: {Tile5.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile5.Value.Description} = {tile.Description}");
                    Tile5.Value = tile.Clone() as Tile;
                    break;
                case 6:
                    Console.WriteLine($"Tile{i}: {Tile6.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile6.Value.Description} = {tile.Description}");
                    Tile6.Value = tile.Clone() as Tile;
                    break;
                case 7:
                    Console.WriteLine($"Tile{i}: {Tile7.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile7.Value.Description} = {tile.Description}");
                    Tile7.Value = tile.Clone() as Tile;
                    break;
                case 8:
                    Console.WriteLine($"Tile{i}: {Tile8.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile8.Value.Description} = {tile.Description}");
                    Tile8.Value = tile.Clone() as Tile;
                    break;
                case 9:
                    Console.WriteLine($"Tile{i}: {Tile9.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile9.Value.Description} = {tile.Description}");
                    Tile9.Value = tile.Clone() as Tile;
                    break;
                case 10:
                    Console.WriteLine($"Tile{i}: {Tile10.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile10.Value.Description} = {tile.Description}");
                    Tile10.Value = tile.Clone() as Tile;
                    break;
                case 11:
                    Console.WriteLine($"Tile{i}: {Tile11.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile11.Value.Description} = {tile.Description}");
                    Tile11.Value = tile.Clone() as Tile;
                    break;
                case 12:
                    Console.WriteLine($"Tile{i}: {Tile12.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile12.Value.Description} = {tile.Description}");
                    Tile12.Value = tile.Clone() as Tile;
                    break;
                case 13:
                    Console.WriteLine($"Tile{i}: {Tile13.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile13.Value.Description} = {tile.Description}");
                    Tile13.Value = tile.Clone() as Tile;
                    break;
                case 14:
                    Console.WriteLine($"Tile{i}: {Tile14.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile14.Value.Description} = {tile.Description}");
                    Tile14.Value = tile.Clone() as Tile;
                    break;
                case 15:
                    Console.WriteLine($"Tile{i}: {Tile15.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile15.Value.Description} = {tile.Description}");
                    Tile15.Value = tile.Clone() as Tile;
                    break;
                case 16:
                    Console.WriteLine($"Tile{i}: {Tile16.Value.Description} = {tile.Description}");
                    Trace.WriteLine($"Tile{i}: {Tile16.Value.Description} = {tile.Description}");
                    Tile16.Value = tile.Clone() as Tile;
                    break;
            }
        }

        private static unsafe void Paste(OpenCvSharp.Mat target, OpenCvSharp.Mat pasting, Rect rect)
        {
            Debug.Assert(pasting.Width == rect.Width);
            Debug.Assert(pasting.Height == rect.Height);
            Debug.Assert(target.Type() == pasting.Type());

            int yy = 0;
            for (int y = 0; y < target.Height; y++)
            {
                byte* p_t = (byte*)target.Ptr(y);
                if (y >= rect.Y && y < rect.Y + rect.Height)
                {
                    byte* p_p = (byte*)pasting.Ptr(yy);
                    int xx = 0;
                    for (int x = 0; x < target.Width; x++)
                    {
                        if (x >= rect.X && x < rect.X + rect.Width)
                        {
                            for (int c = 0; c < target.Channels(); c++)
                            {
                                *(p_t + x * target.Channels() + c) = *(p_p + xx * pasting.Channels() + c);
                            }
                            xx++;
                        }
                    }
                    yy++;
                }
            }
        }

        private OpenCvSharp.Mat getHomography(OpenCvSharp.KeyPoint[] kpsA, OpenCvSharp.KeyPoint[] kpsB, OpenCvSharp.Mat featuresA, OpenCvSharp.Mat featuresB, OpenCvSharp.DMatch[] matches, int reprojThresh)
        {
            //# convert the keypoints to numpy arrays
            if (matches.Length > 4)
            {
                List<OpenCvSharp.Point2f> PtA = new List<OpenCvSharp.Point2f>(matches.Length);
                List<OpenCvSharp.Point2f> PtB = new List<OpenCvSharp.Point2f>(matches.Length);
                foreach (var m in matches)
                {
                    OpenCvSharp.KeyPoint kpsAI = kpsA[m.QueryIdx];
                    OpenCvSharp.KeyPoint kpsBI = kpsB[m.TrainIdx];

                    PtA.Add(new OpenCvSharp.Point2f(kpsAI.Pt.X, kpsAI.Pt.Y));
                    PtB.Add(new OpenCvSharp.Point2f(kpsBI.Pt.X, kpsBI.Pt.Y));
                }
                OpenCvSharp.InputArray ptsA = OpenCvSharp.InputArray.Create(PtA);
                OpenCvSharp.InputArray ptsB = OpenCvSharp.InputArray.Create(PtB);

                //# estimate the homography between the sets of points
                //step 1: find the homography H with findHomography you will get a classic structure for homography
                OpenCvSharp.Mat H = OpenCvSharp.Cv2.FindHomography(ptsA, ptsB, OpenCvSharp.HomographyMethods.Ransac, reprojThresh);

                Console.WriteLine("Homography:");
                for (var rowIndex = 0; rowIndex < H.Rows; rowIndex++)
                {
                    for (var colIndex = 0; colIndex < H.Cols; colIndex++)
                    {
                        double hVal = H.At<double>(rowIndex, colIndex);
                        Console.Write($"{hVal} ");
                    }
                    Console.WriteLine("");
                }

                return H;
            }
            else
                return null;
        }

        private unsafe static OpenCvSharp.Mat Negate(OpenCvSharp.Mat target)
        {
            var mat = target.Clone();

            int channels = mat.Channels();
            for (int y = 0; y < mat.Height; y++)
            {
                byte* p = (byte*)mat.Ptr(y);
                for (int x = 0; x < mat.Width; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        *(p + x * channels + c) = (byte)(255 - *(p + x * channels + c));
                    }
                }
            }
            return mat;
        }

        private unsafe static void Mask(OpenCvSharp.Mat clientRectPool, OpenCvSharp.Mat m, OpenCvSharp.Mat white)
        {
            Debug.Assert(clientRectPool.Size() == m.Size());
            Debug.Assert(clientRectPool.Size() == white.Size());

            int channels = clientRectPool.Channels();
            for (int y = 0; y < clientRectPool.Height; y++)
            {
                byte* p_crp = (byte*)clientRectPool.Ptr(y);
                byte* p_m = (byte*)m.Ptr(y);
                byte* p_w = (byte*)white.Ptr(y);
                for (int x = 0; x < clientRectPool.Width; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        *(p_m + x * channels + c) = Math.Min(*(p_crp + x * channels + c), *(p_w + x * channels + c));
                    }
                }
            }
        }

        private unsafe bool AreEqual(OpenCvSharp.Mat m, OpenCvSharp.Mat target)
        {
            using (OpenCvSharp.Mat temp = new OpenCvSharp.Mat())
            {
                if (m.Size() != target.Size())
                    return false;
                if (m.Channels() != target.Channels())
                    return false;

                int channels = m.Channels();
                for (int y = 0; y < m.Height; y++)
                {
                    byte* p_m = (byte*)m.Ptr(y);
                    byte* p_target = (byte*)target.Ptr(y);
                    for (int x = 0; x < m.Width; x++)
                    {
                        for (int c = 0; c < channels; c++)
                        {
                            var a = *(p_m + x * channels + c);
                            var b = *(p_target + x * channels + c);
                            if (a != b)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private unsafe OpenCvSharp.Mat Fill(OpenCvSharp.Mat target, int fromInclusive, int toExclusive, bool vertical, OpenCvSharp.Scalar fillColor)
        {
            var mat = new OpenCvSharp.Mat(target, new OpenCvSharp.Rect(0, 0, target.Width, target.Height));
            int channels = mat.Channels();
            byte* p;
            for (int y = 0; y < mat.Height; y++)
            {
                if (vertical)
                {
                    if (fromInclusive <= y && y < toExclusive)
                    {
                        p = (byte*)mat.Ptr(y);
                        for (int x = 0; x < mat.Width; x++)
                        {
                            *(p + x * channels + 0) = (byte)fillColor.Val0;
                            *(p + x * channels + 1) = (byte)fillColor.Val1;
                            *(p + x * channels + 2) = (byte)fillColor.Val2;
                        }
                    }
                }
                p = (byte*)mat.Ptr(y);
                for (int x = 0; x < mat.Width; x++)
                {
                    if (!vertical && fromInclusive <= x && x < toExclusive)
                    {
                        *(p + x * channels + 0) = (byte)fillColor.Val0;
                        *(p + x * channels + 1) = (byte)fillColor.Val1;
                        *(p + x * channels + 2) = (byte)fillColor.Val2;
                    }
                }
            }
            return mat;
        }

        private unsafe OpenCvSharp.Mat Subtract(OpenCvSharp.Mat previous, OpenCvSharp.Mat roi)
        {
            Debug.Assert(roi.Channels() == previous.Channels());
            Debug.Assert(roi.Size() == previous.Size());
            Debug.Assert(roi.Depth() == previous.Depth());

            var ret = new OpenCvSharp.Mat(roi.Rows, roi.Cols, roi.Type());
            int channels = roi.Channels();
            for (int y = 0; y < roi.Rows; y++)
            {
                byte* p_r = (byte*)ret.Ptr(y);
                byte* p_roi = (byte*)roi.Ptr(y);
                byte* p_p = (byte*)previous.Ptr(y);
                for (int x = 0; x < roi.Cols; x++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        byte result = (byte)(*(p_roi + x * channels + c) - *(p_p + x * channels + c));
                        *(p_r + x * channels + c) = result > 0 ? (byte)result : (byte)0;
                    }
                }
            }
            return ret;
        }

        private static TemplateCollection _Templates;

        private TemplateCollection Templates
        {
            get
            {
                if (_Templates != null)
                    return _Templates;
                var templateCollection = new TemplateCollection();
                templateCollection.Add(new Template("m1", "Assets/m1.png"));
                templateCollection.Add(new Template("m2", "Assets/m2.png"));
                templateCollection.Add(new Template("m3", "Assets/m3.png"));
                templateCollection.Add(new Template("m4", "Assets/m4.png"));
                templateCollection.Add(new Template("m5", "Assets/m5.png"));
                templateCollection.Add(new Template("m5r", "Assets/m5r.png"));
                templateCollection.Add(new Template("m6", "Assets/m6.png"));
                templateCollection.Add(new Template("m7", "Assets/m7.png"));
                templateCollection.Add(new Template("m8", "Assets/m8.png"));
                templateCollection.Add(new Template("m9", "Assets/m9.png"));
                templateCollection.Add(new Template("p1", "Assets/p1.png"));
                templateCollection.Add(new Template("p2", "Assets/p2.png"));
                templateCollection.Add(new Template("p3", "Assets/p3.png"));
                templateCollection.Add(new Template("p4", "Assets/p4.png"));
                templateCollection.Add(new Template("p5", "Assets/p5.png"));
                templateCollection.Add(new Template("p5r", "Assets/p5r.png"));
                templateCollection.Add(new Template("p6", "Assets/p6.png"));
                templateCollection.Add(new Template("p7", "Assets/p7.png"));
                templateCollection.Add(new Template("p8", "Assets/p8.png"));
                templateCollection.Add(new Template("p9", "Assets/p9.png"));
                templateCollection.Add(new Template("s1", "Assets/s1.png"));
                templateCollection.Add(new Template("s2", "Assets/s2.png"));
                templateCollection.Add(new Template("s3", "Assets/s3.png"));
                templateCollection.Add(new Template("s4", "Assets/s4.png"));
                templateCollection.Add(new Template("s5", "Assets/s5.png"));
                templateCollection.Add(new Template("s5r", "Assets/s5r.png"));
                templateCollection.Add(new Template("s6", "Assets/s6.png"));
                templateCollection.Add(new Template("s7", "Assets/s7.png"));
                templateCollection.Add(new Template("s8", "Assets/s8.png"));
                templateCollection.Add(new Template("s9", "Assets/s9.png"));
                templateCollection.Add(new Template("中", "Assets/中.png"));
                templateCollection.Add(new Template("北", "Assets/北.png"));
                templateCollection.Add(new Template("南", "Assets/南.png"));
                templateCollection.Add(new Template("東", "Assets/東.png"));
                templateCollection.Add(new Template("發", "Assets/發.png"));
                templateCollection.Add(new Template("白", "Assets/白.png"));
                templateCollection.Add(new Template("西", "Assets/西.png"));
                _Templates = templateCollection;
                return templateCollection;
            }
        }
    }
}
