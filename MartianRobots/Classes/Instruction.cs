using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace MartianRobots.Classes
{
    public interface IInstruction 
    {
        void ExecuteInstruction(Robot r, IList<Scent> s, Mars m);
    }
    public class Instruction:IInstruction
    {
        public char Key { get; set; }

        public string Description { get; set; }

        public virtual void ExecuteInstruction(Robot r, IList<Scent> s, Mars m)
        {
            throw new NotImplementedException();
        }
    }

    public class InstructionCode:Instruction
    {
        public Delegate Code { get; set; }

        public override void ExecuteInstruction(Robot r, IList<Scent> s, Mars m)
        {
            Code.DynamicInvoke(new object[] { r, s, m });
        }
    }

    public class InstructionCustom : Instruction
    {
        public String customCode { get; set; }

        public override void ExecuteInstruction(Robot r, IList<Scent> s, Mars m)
        {
            var options = ScriptOptions.Default.AddReferences(typeof(Instruction).Assembly);
 
            Task<Action<Robot, IList<Scent>, Mars>> method = CSharpScript.EvaluateAsync<Action<Robot, IList<Scent>, Mars>> (customCode, options);
            method.Result.DynamicInvoke(new object[] { r, s, m });
        }
    }


}
