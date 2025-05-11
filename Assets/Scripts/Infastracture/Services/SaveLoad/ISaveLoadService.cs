﻿using Data;

namespace Infastracture.Services.SaveLoad
{
    public interface ISaveLoadService: IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}