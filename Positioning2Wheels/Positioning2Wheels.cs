using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        Location robotLocation = new Location(0, 0, 0, 0, 0, 0);

        int robotId;

        public Positioning2Wheels(int id)
        {
            this.robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            // à faire : on calcul
            robotLocation.X = robotLocation.X + (e.Vx/50)*Math.Cos(robotLocation.Theta);
            robotLocation.Y = robotLocation.Y + (e.Vx/50)*Math.Sin(robotLocation.Theta);
            robotLocation.Theta = robotLocation.Theta + (e.Vtheta/50);

            OnCalculatedLocation(robotId, robotLocation);
        }

        //Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
