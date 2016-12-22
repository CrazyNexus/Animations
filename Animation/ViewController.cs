//
// ViewController.cs
//
// Created by Thomas Dubiel on 22.12.2016
// Copyright 2016 Thomas Dubiel. All rights reserved.
//
using System;

using UIKit;
using CoreGraphics;
using CoreAnimation;

namespace Animation
{
	public partial class ViewController : UIViewController
	{
		UIImageView img1;
		CALayer layer;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			layer = new CALayer();
			layer.Bounds = new CGRect(0, 0, 50, 50);
			layer.Position = new CGPoint(20, 200);
			layer.Contents = UIImage.FromFile("Passbild_smal.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResizeAspectFill;
			layer.BorderWidth = 2.5f;
			layer.BorderColor = UIColor.Blue.CGColor;

			View.Layer.AddSublayer(layer);

			newButton.TouchUpInside += NewButton_TouchUpInside;

			img1 = new UIImageView(new CGRect(10, 100, 100, 100));
			img1.ContentMode = UIViewContentMode.ScaleAspectFill;
			img1.Image = UIImage.FromFile("Passbild_smal.png");
			View.AddSubview(img1);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// animation
			var pt = img1.Center;

			UIView.Animate(
				duration: 3,
				delay: 0,
				options: UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
				animation: () =>
				{
					img1.Center = new CGPoint(View.Bounds.GetMaxX() - img1.Frame.Width / 2, pt.Y);
				},
				completion: () =>
				{
					img1.Center = pt;
				}
			);

			//CATransaction.Begin();
			//CATransaction.AnimationDuration = 10;
			//layer.Position = new CGPoint(50, 400);
			//layer.BorderWidth = 6f;
			//layer.BorderColor = UIColor.Red.CGColor;
			//CATransaction.Commit();

			CGPoint fromPoint = layer.Position;
			layer.Position = new CGPoint(200, 300);

			CGPath path = new CGPath();
			path.AddLines(new CGPoint[] { fromPoint, new CGPoint(50, 400), new CGPoint(150, 50), new CGPoint(200, 300) });

			CAKeyFrameAnimation anim = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath("position");
			anim.Path = path;
			anim.Duration = 3;

			layer.AddAnimation(anim, "position");
		}

		void NewButton_TouchUpInside(object sender, EventArgs e)
		{
			SecondViewController vc = new SecondViewController();
			vc.ModalTransitionStyle = UIModalTransitionStyle.PartialCurl;
			this.PresentViewControllerAsync(vc, true);
		}
	}
}
