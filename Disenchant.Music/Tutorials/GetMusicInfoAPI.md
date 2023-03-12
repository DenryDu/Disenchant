
## C# ��ȡ���������Ϣ

֧�ִ󲿷ָ�ʽ�ķ���: 

### 1. Microsoft Shell Controls And Automation

#### ���뷽��
- ����COM���
- ��������ӣ�using Shell32;

#### ʹ�÷���

##### ����ʾ��

```
    string[] Info = new string[7];
    //Shell ����
    ShellClass sh = new ShellClass();

    //Shell�ļ��ж���
    Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(path));

    //Shell�ļ�����
    FolderItem item = dir.ParseName(System.IO.Path.GetFileName(path));

    //��ʼȫ�����Shell�ļ�����
    Info[0] = dir.GetDetailsOf(item, 21);
    //Info[1] = dir.GetDetailsOf(item, 20);
    Info[6] = dir.GetDetailsOf(item, 14);
    Info[3] = dir.GetDetailsOf(item, 27);
    Info[3]= Info[3].Substring(Info[3].IndexOf(":") + 1);
    Info[4] = dir.GetDetailsOf(item, 1);
```

##### ��������

```
ID  => DETAIL-NAME
      0   => Name     //�ļ���
      1   => Size     //�ļ���С
      2   => Type     //�ļ���ʽ
      3   => Date modified
      4   => Date created
      5   => Date accessed
      6   => Attributes
      7   => Offline status
      8   => Offline availability
      9   => Perceived type
      10  => Owner
      11  => Kinds
      12  => Date taken
      13  => Artists   //������
      14  => Album     //ר��
      15  => Year      //�������
      16  => Genre
      17  => Conductors
      18  => Tags
      19  => Rating
      20  => Authors   //ͬ������
      21  => Title     //����
      22  => Subject
      23  => Categories
      24  => Comments
      25  => Copyright
      26  => #         //���
      27  => Length    //ʱ��
      28  => Bit rate
      29  => Protected
      30  => Camera model
      31  => Dimensions
      32  => Camera maker
      33  => Company
      34  => File description
      35  => Program name
      36  => Duration
      37  => Is online
      38  => Is recurring
      39  => Location
      40  => Optional attendee addresses
      41  => Optional attendees
      42  => Organizer address
      43  => Organizer name
      44  => Reminder time
      45  => Required attendee addresses
      46  => Required attendees
      47  => Resources
      48  => Free/busy status
      49  => Total size
      50  => Account name
      51  => Computer
      52  => Anniversary
      53  => Assistant's name
      54  => Assistant's phone
      55  => Birthday
      56  => Business address
      57  => Business city
      58  => Business country/region
      59  => Business P.O. box
      60  => Business postal code
      61  => Business state or province
      62  => Business street
      63  => Business fax
      64  => Business home page
      65  => Business phone
      66  => Callback number
      67  => Car phone
      68  => Children
      69  => Company main phone
      70  => Department
      71  => E-mail Address
      72  => E-mail2
      73  => E-mail3
      74  => E-mail list
      75  => E-mail display name
      76  => File as
      77  => First name
      78  => Full name
      79  => Gender
      80  => Given name
      81  => Hobbies
      82  => Home address
      83  => Home city
      84  => Home country/region
      85  => Home P.O. box
      86  => Home postal code
```

#### �÷������ڵ�ȱ��

-  �������ֵ�����������Ϊ�գ����������ڳ�Ƭ��������������
- �޷�����ר������

### 2. Taglib-sharp
ʹ�÷������£�

��ȡר�������Һ���������Ϣ

```
    //TagLib�ļ�����
    TagLib.File f = TagLib.File.Create(path);

    //��ȡר���������б�
    if (f.Tag.AlbumArtists.Length != 0)
        Info[1] = f.Tag.AlbumArtists[0];
    else
        Info[1] = "";

    //��ȡ�������б�
    if (f.Tag.Artists.Length != 0)
        Info[5] = f.Tag.Artists[0];
    else
        Info[5] = "";

    //��ȡר������
    Info[2] = f.Tag.Album;
```

��ȡ����

- ��ȡ�������ݱ�����

```
    //��ȡר������
    public static MemoryStream GetCover(string path)
    {
        TagLib.File f = TagLib.File.Create(path);
        if (f.Tag.Pictures != null && f.Tag.Pictures.Length != 0)
        {
            var bin = (byte[])(f.Tag.Pictures[0].Data.Data);
            return new MemoryStream(bin);
        }
        else
            return null;
    }
```

- �����������°�װ��ͼƬ
```
    var ss = GetMusicInfo.GetCover(path);
    if (ss != null)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.StreamSource = ss;
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.EndInit();
        bitmap.Freeze();
        image.Source = bitmap;
    }     
```

����ר������

    public static void SetCover(string path)
    {
        TagLib.File file = TagLib.File.Create(path);
        TagLib.Picture pic = new TagLib.Picture();
        pic.Type = TagLib.PictureType.FrontCover;
        pic.Description = "Cover";
        pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
        pic.Data = new TagLib.ByteVector(System.IO.File.ReadAllBytes("D://aaa//aaa.jpg"));
        file.Tag.Pictures = new TagLib.IPicture[] { pic };
        file.Save();
    }

Tag���кö����Կ��Ի�ȡ
����Taglib-sharp�����ı����֧�ֲ��ã����������
�������з������ʹ��

    Album = song.Album;
    AlbumArtists = song.AlbumArtists;
    AlbumArtistsSort = song.AlbumArtistsSort;
    AlbumSort = song.AlbumSort;
    AmazonId = song.AmazonId;
    TitleSort = song.TitleSort;
    Track = song.Track;
    TrackCount = song.TrackCount;
    ReplayGainTrackGain = song.ReplayGainTrackGain;
    ReplayGainTrackPeak = song.ReplayGainTrackPeak;
    ReplayGainAlbumGain = song.ReplayGainAlbumGain;
    ReplayGainAlbumPeak = song.ReplayGainAlbumPeak;
    Comment = song.Comment;
    Disc = song.Disc;
    Composers = song.Composers;
    ComposersSort = song.ComposersSort;
    Conductor = song.Conductor;
    DiscCount = song.DiscCount;
    Copyright = song.Copyright;
    Genres = song.Genres;
    Grouping = song.Grouping;
    Lyrics = song.Lyrics;
    Performers = song.Performers;
    PerformersSort = song.PerformersSort;
    Year = song.Year;
    SampleRate = tagTemp.Properties.AudioSampleRate;
    AudioChannels = tagTemp.Properties.AudioChannels;

https://blog.csdn.net/qq_22033759/article/details/51968045

https://blog.csdn.net/u013419838/article/details/108489023?utm_medium=distribute.pc_relevant.none-task-blog-2~default~baidujs_baidulandingword~default-0-108489023-blog-51968045.pc_relevant_landingrelevant&spm=1001.2101.3001.4242.1&utm_relevant_index=3