using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinaryLib.AttackService
{

  public interface IAttackServiceHost
  {

    void Register(IAttackService attackService);

  }
}
