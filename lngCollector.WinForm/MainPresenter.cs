using lngCollector.Model;
using lngCollector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.WinForm
{
    public class MainPresenter
    {
        IMainView _view;
        IEWordRepo _repo;

        public MainPresenter(IMainView mv, IEWordRepo r)
        {
            _view = mv;
            _repo = r;

            _wireEvents();
            _view.Display(_repo.GetAll());
        }

        private void _wireEvents()
        {
            _view.SaveWord += _view_SaveWord;
            _view.DeleteWord += _view_DeleteWord;
        }

        private void _view_DeleteWord(object? sender, EWord e)
        {
            throw new NotImplementedException();
        }

        private void _view_SaveWord(object? sender, EWord e)
        {
            if (_repo.Save(e) == 0) _view.ShowMsg("word could not be stored");
            _view.Display(_repo.GetAll());
        }
    }
}
