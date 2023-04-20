namespace Helpers.ExtensionsAndDS
{
    [System.Serializable]
    public struct DoAnimationParams<TCurve>
    {
        public float Duration;
        public TCurve Ease;
    }

    [System.Serializable]
    public struct DoScaleParams<TScale, TCurve>
    {
        public TScale Scale;
        public float Duration;
        public TCurve Ease;
    }

    [System.Serializable]
    public struct DoJumpParams<TCurve>
    {
        public float Power;
        public int NumJumps;
        public float Duration;
        public TCurve Ease;
    }

    [System.Serializable]
    public struct DoPunchParams<TScale>
    {
        public TScale Scale;
        public float Duration;
        public int Vibrato;
        public float Elasticity;
    }

    [System.Serializable]
    public struct DoTransformParams<TMoveCurve, TRotateCurve>
    {
        public float Duration;
        public TMoveCurve MoveEase;
        public TRotateCurve RotateEase;
    }
}
