using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Enums;
using MartianRobots.Helpers;
using MartianRobots.Interfaces;

namespace MartianRobots.Classes
{
    public class Robot:IMovable
    {
        #region ctor
        public Robot(string name, Orientation orientation)
        {
            _name = name;
            _isLost = false;
            //_position = initialPosition;
            _orientation = orientation;
        }
        #endregion

        #region Robot Orientation
        //Property read-only any change in orientation using public Methods
        private Orientation _orientation;
        public Orientation Orientation 
        { 
            get { return _orientation; }}
        #endregion

        #region Property Name
        private String _name;
        public String Name {
            get { return _name; }
        }
        #endregion

        #region IsLost
        private bool _isLost;
        public bool IsLost { 
            get { return _isLost; }
        }
        #endregion


        //In a first version, I included 2 properties for the position of the Robot and the previous position to use 
        //when the robot was lost and position was out-of-bounds
        #region Property Position
        //private Tuple<int, int> _position;
        //public Tuple<int,int> Position {
        //    get { return _position; }
        //}
        #endregion region

        #region Previous Position
        //private Tuple<int, int> _previousPosition;
        //public Tuple<int, int> PreviousPosition
        //{
        //    get { return _previousPosition; }
        //}
        #endregion


        #region Public Methods
        public void TurnRight()
        {
            _orientation = OrientationHelper.ChangeOrientation(_orientation, 'R');
        }

        public void TurnLeft()
        {
            _orientation = OrientationHelper.ChangeOrientation(_orientation, 'L');
        }

        public void MoveForward()
        {
            //_previousPosition = _position;
            //if (_orientation == Orientation.N)
            //{
            //    _position =new Tuple<int,int>(_position.Item1, _position.Item2 + 1);
            //}
            //else if (_orientation == Orientation.E)
            //{
            //    _position = new Tuple<int, int>(_position.Item1+1, _position.Item2);
            //}
            //else if (_orientation == Orientation.S)
            //{
            //    _position = new Tuple<int, int>(_position.Item1, _position.Item2 - 1);
            //}
            //else if (_orientation == Orientation.W)
            //{
            //    _position = new Tuple<int, int>(_position.Item1 - 1, _position.Item2);
            //}
            try 
            {
                MoveForwardEvent.Invoke(this, _orientation);
            }
            catch(IndexOutOfRangeException e)
            {

                this._isLost = true;
            }
            
        }
        #endregion


        #region events
        public delegate void Notify(Robot robot,Orientation orientation);

        public event Notify MoveForwardEvent;
        #endregion
    }
}
