// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

#include "pch.h"
#include "SonglistPage.xaml.h"
#if __has_include("SonglistPage.g.cpp")
#include "SonglistPage.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace Windows::UI::ViewManagement;
using namespace Windows::UI::Core;
using namespace Windows::ApplicationModel::Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::DisenchantMusicPlayer::implementation
{
    SonglistPage::SonglistPage()
    {
        InitializeComponent();

        //auto myDataList = winrt::single_threaded_observable_vector<SonglistItem>();
        std::vector<Windows::Foundation::IInspectable> myDataList;
        std::vector<SonglistItem> SonglistData;
        SonglistItem item1 = SonglistItem::SonglistItem(L"Song1",L"Album1",L"ms-appx:///Assets/Images/Cover.jpg");
        SonglistItem item2 = SonglistItem::SonglistItem(L"Song2",L"Album2",L"ms-appx:///Assets/Images/Cover.jpg");
        SonglistData.push_back(item1);
        SonglistData.push_back(item2);
        SonglistView().ItemsSource(single_threaded_observable_vector(std::move(SonglistData)));

        //auto view = ApplicationView::GetForCurrentView();
        // If this size is not permitted by the system, the nearest permitted value is used.
        //view.SetPreferredMinSize({ 600, 200 });
    }
    void SonglistPage::OnNavigatedTo(Microsoft::UI::Xaml::Navigation::NavigationEventArgs const& e) {

        //// ��ȡ��ǰ��ͼ
        //auto currentView = winrt::Windows::ApplicationModel::Core::CoreApplication::GetCurrentView();
        //// ��ȡ��ǰ����
        //auto currentWindow = currentView.CoreWindow();
        //// ��ȡ������
        //auto titleBar = winrt::Windows::ApplicationModel::Core::CoreApplication::GetCurrentView().TitleBar();
        //// ���������쵽������
        //titleBar.ExtendViewIntoTitleBar(true);

        // ��ȡ��ǰӦ�ó�����ͼ
        //auto view = ApplicationView::GetForCurrentView();
        // ����һ��������С�ߴ�� Size ���� Size minSize(500, 0);
        // ���ô��ڵ���С�ߴ�
        //view.SetPreferredMinSize({ 300,200 });
    }
    int32_t SonglistPage::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void SonglistPage::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    
}

