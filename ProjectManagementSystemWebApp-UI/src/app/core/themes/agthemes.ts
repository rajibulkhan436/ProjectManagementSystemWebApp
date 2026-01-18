import { themeQuartz } from 'ag-grid-community';

export const AgTheme = themeQuartz.withParams({
  accentColor: '#087AD1',
  backgroundColor: '#FFFFFF',
  borderColor: '#D7E2E6',
  borderRadius: 2,
  browserColorScheme: 'light',
  cellHorizontalPaddingScale: 0.7,
  chromeBackgroundColor: {
    ref: 'backgroundColor',
  },
  columnBorder: false,
  fontFamily: {
    googleFont: 'Inter',
  },
  fontSize: 14,
  foregroundColor: '#555B62',
  headerBackgroundColor: '#FFFFFF',
  headerFontSize: 18,
  headerFontWeight: 400,
  headerTextColor: '#323336',

  rowHeight: Math.floor(window.innerHeight / 15),
  rowBorder: true,
  rowVerticalPaddingScale: 0,

  sidePanelBorder: true,
  spacing: 6,
  wrapperBorder: false,
  wrapperBorderRadius: 2,
});
