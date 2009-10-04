using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projection
{
    public class GoogleMapsAPIProjection
    {
        private PointF[] Fx = new PointF[0];
        private double[] Hx = new double[0];
        private double[] Ix = new double[0];

        public GoogleMapsAPIProjection(int maxZoomLevel)
        {
            //function Pg(a){
            //    var b=this;
            //    b.Hx=[];b.Ix=[];b.Fx=[];b.Gx=[];
            //    var c=256;
            //    for(var d=0;d<a;d++){
            //        var e=c/2;
            //        b.Hx.push(c/360);
            //        b.Ix.push(c/(2*sd));
            //        b.Fx.push(new L(e,e));
            //        b.Gx.push(c);
            //        c*=2
            //    }
            //}
            var c = 256d;
            for (var d = 0; d < maxZoomLevel; d++)
            {
                var e = Convert.ToSingle(c / 2d);
                this.Hx = this.Hx.Concat(new double[] { c / 360d }).ToArray();
                this.Ix = this.Ix.Concat(new double[] { c / (2d * Math.PI) }).ToArray();
                this.Fx = this.Fx.Concat(new PointF[] { new PointF(e, e) }).ToArray();
                c *= 2d;
            }
        }

        private double Pd(double a, double b, double c)
        {
            //function Pd(a,b,c){
            //    if(b!=null)a=Dd(a,b);
            //    if(c!=null)a=Ed(a,c);
            //    return a
            //}
            return Math.Min(Math.Max(a, b), c);
        }

        private double re(double a)
        {
            //function re(a){return a*(sd/180)} 
            return a * (Math.PI / 180);
        }

        private double se(double a)
        {
            //function se(a){return a/(sd/180)}
            return a / (Math.PI / 180);
        }

        #region IProjection Members

        public PointF FromCoordinatesToPixel(PointF coordinates, int zoomLevel)
        {
            //Pg.prototype.fromLatLngToPixel=function(a,b){
            //    var c=this,
            //    d=c.Fx[b],
            //    e=z(d.x+a.lng()*c.Hx[b]),
            //    f=Pd(Math.sin(re(a.lat())),-0.9999,0.9999),
            //    g=z(d.y+0.5*Math.log((1+f)/(1-f))*-c.Ix[b]);
            //    return new L(e,g)
            //};
            var c = this;
            var d = c.Fx[zoomLevel];
            var e = Math.Round(d.X + (coordinates.X * c.Hx[zoomLevel]));
            var f = Pd(Math.Sin(re(coordinates.Y)), -0.9999d, 0.9999d);
            var g = Math.Round(d.Y + .5d * Math.Log((1d + f) / (1d - f)) * -c.Ix[zoomLevel]);
            return new PointF(Convert.ToSingle(e), Convert.ToSingle(g));
        }

        public PointF FromPixelToCoordinates(PointF pixel, int zoomLevel)
        {
            //Pg.prototype.fromPixelToLatLng=function(a,b,c){
            //    var d=this,
            //    e=d.Fx[b],
            //    f=(a.x-e.x)/d.Hx[b],
            //    g=(a.y-e.y)/-d.Ix[b],
            //    h=se(2*Math.atan(Math.exp(g))-sd/2);
            //    return new K(h,f,c)
            //};
            var d = this;
            var e = d.Fx[zoomLevel];
            var f = (pixel.X - e.X) / d.Hx[zoomLevel];
            var g = (pixel.Y - e.Y) / -d.Ix[zoomLevel];
            var h = d.se(2 * Math.Atan(Math.Exp(g)) - Math.PI / 2);
            return new PointF(Convert.ToSingle(h), Convert.ToSingle(f));
        }

        #endregion
    }
}
//var qd=window._mStaticPath,rd=qd+"transparent.png",sd=Math.PI,td=Math.abs,ud=Math.asin,vd=Math.atan,wd=Math.atan2,zd=Math.ceil,Ad=Math.cos,Cd=Math.floor,Dd=Math.max,Ed=Math.min,Fd=Math.pow,z=Math.round,Gd=Math.sin,Hd=Math.sqrt,Id=Math.tan,Jd="boolean",Kd="number",Ld="object",Md="string",Nd="function",Od="undefined";function j(a){return a.length}