namespace Core.Utilities.Enums
{
    public struct Progress
    {
        public float progressAmount;
        public float progressMax;
        public bool isCompleted;
        public float relativeProgress => progressAmount / progressMax;

        public Progress(float progressAmount = 0, float progressMax = 1, bool isCompleted = false)
        {
            this.progressAmount = progressAmount;
            this.progressMax = progressMax;
            this.isCompleted = isCompleted;
        }
    }
}