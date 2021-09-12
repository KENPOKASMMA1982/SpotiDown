﻿using Newtonsoft.Json;
using SpotiDownVB;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiDown
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tab_settings : ContentPage
    {
        public tab_settings() {  InitializeComponent(); updateui(); }

        private void updateui()
        {
            switch_meta_lyrics.IsToggled = Helper.config.metadata.lyrics;
            switch_meta_artwork.IsToggled = Helper.config.metadata.artwork;

            pck_pref_quality.SelectedIndex = Helper.config.prefernces.quality;
            entry_pref_downloadpath.Text = Helper.config.prefernces.downloadpath;
            entry_pref_filename.Text = Helper.config.prefernces.filename;

            entry_oath_clientid.Text = Helper.config.oauth.id;
            entry_oath_clientsecret.Text = Helper.config.oauth.secret;
        }

        #region meta
        private void switch_meta_lyrics_Toggled(object sender, ToggledEventArgs e) { Helper.config.metadata.lyrics = switch_meta_lyrics.IsToggled; Helper.SaveConfig();}
        private void switch_meta_artwork_Toggled(object sender, ToggledEventArgs e) { Helper.config.metadata.artwork = switch_meta_artwork.IsToggled; Helper.SaveConfig();}
        #endregion

        #region pref
        private void pck_pref_quality_SelectedIndexChanged(object sender, EventArgs e) { Helper.config.prefernces.quality = pck_pref_quality.SelectedIndex; Helper.SaveConfig(); }
        private void entry_pref_downloadpath_Unfocused(object sender, FocusEventArgs e) { entry_pref_downloadpath.Text = Helper.MakePathSafe(entry_pref_downloadpath.Text); Helper.config.prefernces.downloadpath = entry_pref_downloadpath.Text; Helper.SaveConfig(); }
        private void entry_pref_filename_Unfocused(object sender, FocusEventArgs e) { entry_pref_filename.Text = Helper.MakeFileSafe(entry_pref_filename.Text); Helper.config.prefernces.filename = entry_pref_filename.Text; Helper.SaveConfig(); }
        private async void btn_filenameinfo_clicked(object sender, EventArgs e) { await DisplayAlert("Filename Replacements:", "If you use any of these attributes in the filename, they will get replaced with the matching info!\n\n{title} = Song Title\n{artist} = Song Main Artist\n{album} = Song Album\n{release} = Song Release Year", "OK"); ; }
        #endregion

        #region oath
        private void entry_oath_clientid_Unfocused(object sender, FocusEventArgs e) { Helper.config.oauth.id = entry_oath_clientid.Text; Helper.SaveConfig(); }
        private void entry_oath_clientsecret_Unfocused(object sender, FocusEventArgs e) { Helper.config.oauth.secret = entry_oath_clientsecret.Text; Helper.SaveConfig(); }
        #endregion

        private void btn_reset_clicked(object sender, EventArgs e) { Helper.config = new config(); Helper.SaveConfig(); updateui(); }
        private async void btn_show_clicked(object sender, EventArgs e) { await DisplayAlert("Current Config (JSON)", JsonConvert.SerializeObject(Helper.config), "OK"); ; }
    }
}