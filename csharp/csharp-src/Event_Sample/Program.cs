namespace Event_Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Teacher teacher = new Teacher();
            Student student = new Student() { Name = "小明" };
            student.Study += teacher.Lessons;
            student.OnLessons("语文");
            Console.Read();
            student.Rest += teacher.Recess;
            student.StartRest("语文");
            Console.Read();


        }
    }





    /// <summary>
    /// 定义事件参数信息，按照约定事件以EventArgs结尾，且派生自EventArgs
    /// 这里定义学习事件参数
    /// </summary>
    public class StudyEventArgs : EventArgs
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CurriculumName { get; set; }
    }

    /// <summary>
    /// 定义事件约束委托，注意要定义在类的外部，按照约定自定义事件要以EventHandler结尾
    /// 这里定义学习事件，通常事件的两个参数分别为事件拥有者，和事件参数信息
    /// </summary>
    /// <param name="student"></param>
    /// <param name="e"></param>
    public delegate void StudyEventHandler(Student student, StudyEventArgs e);

    /// <summary>
    /// 简明声明事件示例
    /// 这里定义学生休息事件
    /// </summary>
    /// <param name="student"></param>
    /// <param name="e"></param>
    public delegate void RestEventHandler(Student student, StudyEventArgs e);


    /// <summary>
    /// 定义事件拥有者
    /// 这里事件拥有者为学生
    /// </summary>

    public class Student
    {
        public string Name { get; set; }

        private StudyEventHandler studyEventHandler;

        /// <summary>
        /// 定义事件本身
        /// </summary>

        public event StudyEventHandler Study
        {
            add
            {
                this.studyEventHandler += value;
            }
            remove
            {
                this.studyEventHandler -= value;
            }
        }

        /// <summary>
        /// 定义触发事件的方法，开始上课
        /// </summary>

        public void OnLessons(string curriculumName)
        {
            //等于null，就说明该事件没有被订阅
            if (this.studyEventHandler != null)
            {
                //事件必须由事件拥有者触发
                this.studyEventHandler.Invoke(this, new StudyEventArgs()
                {
                    CurriculumName = curriculumName
                });
            }
        }

        #region 事件简略声明形式
        //简略声明的事件是由编译器进行的处理


        /// <summary>
        /// 声明事件
        /// </summary>
        public event RestEventHandler Rest;

        public void StartRest(string curriculumName)
        {
            if(this.Rest != null)
            {
                this.Rest.Invoke(this, new StudyEventArgs() { CurriculumName = curriculumName });

            }
        }

        #endregion



    }

    /// <summary>
    /// 定义事件响应者
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// 上课
        /// </summary>
        /// <param name="student"></param>
        /// <param name="e"></param>
        public void Lessons(Student student, StudyEventArgs e)
        {
            Console.WriteLine($"{student.Name}正在上{e.CurriculumName}课");
        }

        internal void Recess(Student student, StudyEventArgs e)
        {
            Console.WriteLine($"{e.CurriculumName}下课了，{student.Name}开始休息了");
        }
    }
}
