module LeapMouse.FrameApplication

 open System.Windows.Forms
 open System.Drawing

 type Delegate = delegate of string -> unit
 //let deleg = new Delegate(fun s -> app.label.Text <- s)

 type TrayApplication () as this =
            inherit Form()

            let lbl = new Label()
            let btn = new Button()

            do
            
    //            this.MaximizeBox <- true
    //            this.Width <- 1000
    //            this.Height <- 600
                this.FormBorderStyle <- FormBorderStyle.FixedSingle
                lbl.Visible <- true
                lbl.Width <- 200
                lbl.Height <- 40
                this.BackColor <- Color.Azure
                lbl.Location <- new Point(this.Location.X + this.Width / 2 - lbl.Width / 2, this.Location.Y + this.Height / 2 - lbl.Height / 2)
                lbl.Font <- new Font("Verdana", 10.F)
                lbl.Text <- "* Simple Frame *"
                btn.Text <- "Inizia"
                btn.Click.Add(fun e -> System.Console.WriteLine("cane"))
                let t = new TableLayoutPanel()
                t.Controls.Add(lbl)
                t.Controls.Add(btn)
                this.Controls.Add(t)
                this.AutoSize<- true



            member this.label = lbl

            member this.button = btn

            override this.OnLoad(e:System.EventArgs) =
                this.Visible <- true
                this.ShowInTaskbar <- true
                
                base.OnLoad(e)


            member this.PopText(s:string) =
                this.label.Text <- s
 