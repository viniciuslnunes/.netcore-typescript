import styled from 'styled-components'

export const Container = styled.div`
  width: 100%;

  a {
    display: flex;
    align-items: center;
    justify-content: center;

    width: 40px;
    height: 40px;

    padding: 8px;

    text-decoration: none;
    color: inherit;

    border-radius: 24px;
    transition: background-color 0.25s ease;

    &:hover {
      background-color: #ddd;
    }
  }
`

export const MainContent = styled.main`
  width: 100;
  margin: 20px 40px;

  > button {
    margin-left: auto;
  }
`

export const FileList = styled.table`
  width: 100%;
  background: transparent;
  border-spacing: 0 10px;
  
  margin: 20px 0;

  thead th {
    text-align: left;
    color: #444444;
    font-size: 14px;
  }

  th,
  td {
    text-align: left;

    &.icon {
      width: 60px;
      text-align: center;

      svg {
        align-self: center;
      }
    }

    button {
      cursor: pointer;
      border: 0;
      background-color: transparent;
    }
  }
`

export const FlexContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
`
