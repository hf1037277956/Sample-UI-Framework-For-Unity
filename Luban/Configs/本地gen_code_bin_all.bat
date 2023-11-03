set WORKSPACE=..
set GEN_CLIENT=%WORKSPACE%\Tools\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Configs
set DEFINE_FILE=%CONF_ROOT%\Defines\__root__.xml

%GEN_CLIENT% --template_search_path %WORKSPACE%\YouTemplate -j cfg --^
 -d %DEFINE_FILE%^
 --input_data_dir %CONF_ROOT%\Datas ^
 --output_code_dir %WORKSPACE%\..\Assets\Scripts\Config\Generate ^
 --output_data_dir %WORKSPACE%\..\Assets\Resources\Config\cs ^
 --gen_types code_cs_bin,data_bin ^
 --cs:use_unity_vector ^
 --external:selectors client,all ^
 -s client ^
--l10n:timezone "China Standard Time" ^
--l10n:input_text_files L10n/Localization.xlsx ^
--l10n:text_field_name text_cn ^
--l10n:output_not_translated_text_file L10n/NotLocalized_C.txt

pause